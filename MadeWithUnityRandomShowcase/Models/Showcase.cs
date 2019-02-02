using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MadeWithUnityRandomShowcase.Models
{
    public class Showcase
    {
        public string showcaseTitle, showcaseStudio;

        public List<string>
            mainShowcase,
            galleryShowcase,
            backdropURLsShowcase,
            videoURLsShowcase;

        public Showcase()
        {
            mainShowcase = new List<string>();
            galleryShowcase = new List<string>();
            backdropURLsShowcase = new List<string>();
            videoURLsShowcase = new List<string>();
        }
    }
}