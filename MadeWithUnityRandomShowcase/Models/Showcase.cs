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
            showcaseHeadings, 
            showcaseText,
            showcaseImages,
            showcaseCaptions,
            showcaseVideos,
            showcaseURLS;

        public Showcase()
        {
            showcaseHeadings = new List<string>();
            showcaseText = new List<string>();
            showcaseImages = new List<string>();
            showcaseCaptions = new List<string>();
            showcaseVideos = new List<string>();
            showcaseURLS = new List<string>();
        }
    }
}