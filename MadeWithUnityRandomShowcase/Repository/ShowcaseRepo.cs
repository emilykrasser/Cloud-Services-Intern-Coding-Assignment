﻿using HtmlAgilityPack;
using MadeWithUnityRandomShowcase.Models;
using System;
using System.Linq;
using System.Text;

namespace MadeWithUnityRandomShowcase.Repository
{
    public class ShowcaseRepo
    {
        /// <summary>
        /// Screen scrapes given URL and populates showcase object with display content
        /// </summary>
        public static Showcase GetShowcaseInfo(string URL)
        {
            Showcase showcase = new Showcase();

            // Loading in the webpage
            HtmlWeb website = new HtmlWeb();
            website.AutoDetectEncoding = false;
            website.OverrideEncoding = Encoding.UTF8;
            HtmlDocument Doc = website.Load("https://unity.com" + URL);


            // Finding all of the HTML nodes that have to do with the tag <div class="col-xs-12..."> to find the infomation and images
            var mainNodes = Doc.DocumentNode.Descendants().Where(n => n.Attributes.Any(a => a.Value.Contains("col-xs-12")));

            foreach (var i in mainNodes)  // i == <div class="col-xs-12...">
            {
                foreach (var j in i.ChildNodes)
                {
                    if (j.GetAttributeValue("class", "") == "article-image-item")  // j == <div class="article-image-item">
                    {
                        // Set image link(href) absolute path when clicked
                        j.ChildNodes[1].SetAttributeValue("href", "https://unity.com" + j.ChildNodes[1].GetAttributeValue("href", ""));
                        // Set image source(src) absolute path
                        j.ChildNodes[1].ChildNodes[1].SetAttributeValue("src", "https://unity.com" + j.ChildNodes[1].ChildNodes[1].GetAttributeValue("src", ""));
                        showcase.mainShowcase.Add(j.OuterHtml);
                    }
                    else if (j.Name == "img")  // j == <img src="..." alt="..." title="...">
                    {
                        // Set image source(src) absolute path
                        j.SetAttributeValue("src", "https://unity.com" + j.GetAttributeValue("src", ""));
                        showcase.mainShowcase.Add(j.OuterHtml);  // Must use OutterHtlm in this node's case
                    }
                    else if (j.Name != "#text")
                    {
                        showcase.mainShowcase.Add(j.OuterHtml);
                    }
                }
            }


            // Finding all of the HTML nodes that have to do with the tag <div class="section-article-gallery"> to find the galleries
            var galleryNodes = Doc.DocumentNode.Descendants().Where(n => n.Attributes.Any(a => a.Value.Contains("section-article-gallery")));

            foreach (var i in galleryNodes)  // i == <div class="section-article-gallery">
            {
                foreach (var j in i.ChildNodes[1].ChildNodes)  // i.ChildNodes[1] == <div class="row">, j == <div class="col-xs-6 col-sm-4">
                {
                    if (j.HasChildNodes)
                    {
                        // j.ChildNodes == <div class="article-image-item">
                        // Set image link(href) absolute path when clicked
                        j.ChildNodes[1].ChildNodes[1].SetAttributeValue("href", "https://unity.com" + j.ChildNodes[1].ChildNodes[1].GetAttributeValue("href", ""));
                        // Set image source(src) absolute path
                        j.ChildNodes[1].ChildNodes[1].ChildNodes[1].SetAttributeValue("src", "https://unity.com" + j.ChildNodes[1].ChildNodes[1].ChildNodes[1].GetAttributeValue("src", ""));
                        // Adding a class tag for CSS
                        j.ChildNodes[1].ChildNodes[1].ChildNodes[1].Attributes.Add("class", "gallery-image");                    
                    }
                }
                showcase.galleryShowcase.Add(i.InnerHtml);
            }


            // Finding all of the HTML nodes that have to do with the tag <div class="component-gallery-item" data-caption=""> to find the component galleries
            var componentGalleryNodes = Doc.DocumentNode.Descendants().Where(n => n.Attributes.Any(a => a.Value.Contains("component-gallery-item")));

            foreach (var i in componentGalleryNodes)  // i == <div class="component-gallery-item" data-caption="">
            {
                // i.ChildNodes[1] == <div class="container">, i.ChildNodes[1].ChildNodes[1] == <img src="..." alt="" title=""/>
                // Set image source(src) absolute path
                i.ChildNodes[1].ChildNodes[1].SetAttributeValue("src", "https://unity.com" + i.ChildNodes[1].ChildNodes[1].GetAttributeValue("src", ""));

                showcase.componentGalleryShowcase.Add(i.OuterHtml);
            }


            // Finding all of the HTML nodes that have to do with the tag <div class="section section-story-hero" style="background-image: url(...)"> to find the background images
            var backdropNodes = Doc.DocumentNode.Descendants().Where(n => n.Attributes.Any(a => a.Value.Contains("background-image")));

            foreach (var i in backdropNodes)  // i == <div class="section section-story-hero" style="background-image: url(...)">
            {
                var styleText = i.GetAttributeValue("style", "");
                string[] separatingChars = { "(&#039;", "&#039;)", "('", ");"};
                string[] parsedStyleText = styleText.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);

                if (i.GetAttributeValue("class", "") == "section-hero-image")
                {
                    parsedStyleText[1] = "https://unity.com" + parsedStyleText[1];  // Getting the absolute path for the image
                }

                showcase.backdropURLsShowcase.Add(parsedStyleText[1]);
            }


            // Finding all of the HTML nodes that have to do with the tag <div class="section-trailer embed-responsive..."> to find videos
            var videoNodes = Doc.DocumentNode.Descendants().Where(n => n.Attributes.Any(a => a.Value.Contains("section-trailer embed-responsive")));

            foreach (var i in videoNodes)  // i == <div class="section-trailer embed-responsive...">
            {
                var embeddedVidURL = "";
                var vidNode = i.ChildNodes[1];  // <div id="..." class="..." data-yt or data-vm="...">

                if (vidNode.GetAttributeValue("class", "") == "yt-video")
                {
                    embeddedVidURL = "https://www.youtube.com/embed/" + vidNode.GetAttributeValue("data-yt", "");
                }
                else if (vidNode.GetAttributeValue("class", "") == "vm-video")
                {
                    embeddedVidURL = "https://player.vimeo.com/video/" + vidNode.GetAttributeValue("data-vm", "");
                }

                showcase.videoURLsShowcase.Add(embeddedVidURL);
            }

            return showcase;
        }
    }
}