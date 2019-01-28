using HtmlAgilityPack;
using MadeWithUnityRandomShowcase.Models;
using System;
using System.Linq;
using System.Text;

namespace MadeWithUnityRandomShowcase.Repository
{
    public class ShowcaseRepo
    {
        public static Showcase GetShowcaseInfo()
        {
            Showcase showcase = new Showcase();

            // Loading in the webpage
            HtmlWeb website = new HtmlWeb();
            website.AutoDetectEncoding = false;
            website.OverrideEncoding = Encoding.UTF8;
            HtmlDocument Doc = website.Load("https://unity.com/madewith/golf-club-wasteland");

            // Finding the title of the showcase
            showcase.showcaseTitle = Doc.DocumentNode.Descendants()
                .Where(n => n.Attributes
                .Any(a => a.Value.Contains("section-hero-title title-huge gsap-text-1")))
                .First()
                .InnerText;

            // Finding the title of the showcase
            showcase.showcaseStudio = Doc.DocumentNode.Descendants()
                .Where(n => n.Attributes
                .Any(a => a.Value.Contains("section-hero-studio")))
                .First()
                .InnerText;

            // Finding the headings within the website
            var headingsNodes = Doc.DocumentNode.Descendants().Where(n => n.Attributes.Any(a => a.Value.Contains("title-large")));

            // Finding all of the HTML nodes that have to do with the tag <div class="section-article-text"> to find the text for the showcase
            var textNodes = Doc.DocumentNode.Descendants()
                .Where(n => n.Attributes
                .Any(a => a.Value.Contains("section-article-text")));

            foreach (var i in textNodes)
            {
                if (i.HasChildNodes == true)
                {
                    showcase.showcaseText?.Add(i.ChildNodes[1].InnerText);
                }
            }
            return showcase;
        }
    }
}