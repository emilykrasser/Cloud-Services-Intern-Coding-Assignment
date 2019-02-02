using MadeWithUnityRandomShowcase.Models;
using MadeWithUnityRandomShowcase.Repository;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;
using HtmlAgilityPack;
using System.Web.Caching;

namespace MadeWithUnityRandomShowcase.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Finds and adding the URLs for the Made with Unity Projects to a cached list
        /// </summary>
        List<string> GetShowcaseURLS()
        {
            List<string> cachedURLs = HttpContext.Cache.Get("cachedURLs") as List<string>;

            if (HttpContext.Cache.Get("cachedURLs") == null || cachedURLs.Count == 0)
            {
                List<string> URLS = new List<string>();

                // Loading in the webpage
                HtmlWeb website = new HtmlWeb();
                website.AutoDetectEncoding = false;
                website.OverrideEncoding = Encoding.UTF8;
                HtmlDocument Doc = website.Load("https://unity.com/madewith");

                // Finding all the HTML nodes for the URLs
                var URLNodes = Doc.DocumentNode.Descendants()
                    .Where(n => n.Attributes
                    .Any(a => a.Value.Contains("post--story")));

                // Adding the reletive URL paths to the list
                foreach (var i in URLNodes)
                {
                    URLS.Add(i.ChildNodes[1].GetAttributeValue("href", "default"));
                }

                // Add URLS list to the cache with a sliding expriration so that it expires 30 min after the data was last accessed
                HttpContext.Cache.Add("cachedURLs", URLS, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 30, 0), CacheItemPriority.Normal, null);
            }

            cachedURLs = HttpContext.Cache.Get("cachedURLs") as List<string>;

            return cachedURLs;
        }

        /// <summary>
        /// Retrieves random index from cached list of unseen indices
        /// </summary>
        int GetURLIndicies(int numOfURLs)
        {
            List<int> cachedIndices = HttpContext.Cache.Get("unseenIndices") as List<int>;

            if (HttpContext.Cache.Get("unseenIndices") == null || cachedIndices.Count == 0)
            {
                List<int> indexArr = new List<int>();
                for (int i = 0; i < numOfURLs; i++)
                {
                    indexArr.Add(i);
                }

                // No need to set a sliding expiration here since we reset the cache later
                HttpContext.Cache["unseenIndices"] = indexArr;
            }

            cachedIndices = HttpContext.Cache.Get("unseenIndices") as List<int>;
            Random rand = new Random();
            int randNum = rand.Next(0, cachedIndices.Count); // Index for the indices

            int randIndex = cachedIndices[randNum]; // Final random index for URLs
            cachedIndices.RemoveAt(randNum); // Removing from list of unseen indices

            // Reseting the cached list with the updated list and a sliding expriration so that it expires 12 hours after the data was last accessed
            HttpContext.Cache.Add("cachedURLs", cachedIndices, null, Cache.NoAbsoluteExpiration, new TimeSpan(12, 0, 0), CacheItemPriority.Normal, null);

            return randIndex;
        }

        public ActionResult Index()
        {
            List<string> urls = GetShowcaseURLS();
            int URLIndex = GetURLIndicies(urls.Count);

            Showcase showcase = ShowcaseRepo.GetShowcaseInfo(urls[URLIndex]);
            return View(showcase);
        }
    }
}