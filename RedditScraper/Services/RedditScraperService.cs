using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Newtonsoft.Json;
using RedditScraper.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace RedditScraper.Services
{
    public class RedditScraperService : IRedditScraperService
    {
        public List<TopScoringReddit> GetAll()
        {
            string url = "https://old.reddit.com/r/all/top/";
            List<TopScoringReddit> results = new List<TopScoringReddit>();

            while (url != null)
            {
                using (var client = new HttpClient())
                {
                    var html = client.GetStringAsync(url).Result;
                    var parser = new HtmlParser();
                    var document = parser.Parse(html);
                    var siteTable = document.QuerySelectorAll("#siteTable > .thing");

                    foreach (var item in siteTable)
                    {
                        var topScoringReddit = new TopScoringReddit();

                        var mayBlank = item.QuerySelector(".entry > .top-matter > .title > .may-blank");
                        topScoringReddit.Title = mayBlank.TextContent;

                        var subReddit = item.QuerySelector(".entry > .top-matter > .tagline > .subreddit");
                        topScoringReddit.SubReddit = subReddit.TextContent;

                        var first = item.QuerySelector(".entry > .top-matter > .flat-list > .first");
                        topScoringReddit.Comments = first.TextContent;

                        results.Add(topScoringReddit);
                    }

                    var aElem = document.QuerySelector("#siteTable > .nav-buttons > .nextprev > .next-button a");

                    if (aElem != null)
                    {
                        url = aElem.GetAttribute("href");
                        Trace.WriteLine(url);
                    }
                    else
                    {
                        url = null;
                    }
                }
            }
            return results;
        }
    }
}