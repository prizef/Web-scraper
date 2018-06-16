using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedditScraper.Models
{
    public class TopScoringReddit
    {
        public string Title { get; set; }
        public string SubReddit { get; set; }
        public string Comments { get; set; }
    }
}