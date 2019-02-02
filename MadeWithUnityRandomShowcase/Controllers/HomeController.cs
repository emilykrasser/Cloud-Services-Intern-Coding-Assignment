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
        /// Finding and adding the URLs for the Made with Unity Projects to a list
        /// </summary>
        /// <returns></returns>
        List<string> GetShowcaseURLS()
        {
            List<string> cachedURLs = HttpContext.Cache.Get("cachedURLs") as List<string>;

            if (HttpContext.Cache["cachedURLs"] == null || cachedURLs.Count == 0)
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

                HttpContext.Cache["cachedURLs"] = URLS;
            }

            cachedURLs = HttpContext.Cache.Get("cachedURLs") as List<string>;

            return cachedURLs;
        }

        int GetURLIndicies(int numOfURLs)
        {
            List<int> cachedIndices = HttpContext.Cache.Get("unseenIndices") as List<int>;

            if (HttpContext.Cache["unseenIndices"] == null || cachedIndices.Count == 0)
            {
                List<int> indexArr = new List<int>();
                for (int i = 0; i < numOfURLs; i++)
                {
                    indexArr.Add(i);
                }

                HttpContext.Cache["unseenIndices"] = indexArr;
            }

            cachedIndices = HttpContext.Cache.Get("unseenIndices") as List<int>;
            Random rand = new Random();
            int randNum = rand.Next(0, cachedIndices.Count); // Index for the indices

            int randIndex = cachedIndices[randNum]; // Final random index for URLs
            cachedIndices.RemoveAt(randNum); // Removing from list of seen indices

            HttpContext.Cache["unseenIndices"] = cachedIndices; // Reseting the cached list with the updated list

            return randIndex;
        }

        public ActionResult Index()
        {
            List<string> urls = GetShowcaseURLS();
            int URLIndex = GetURLIndicies(urls.Count);

            Showcase showcase = ShowcaseRepo.GetShowcaseInfo(urls[URLIndex]);
            return View(showcase);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}