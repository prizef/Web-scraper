using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Newtonsoft.Json;
using RedditScraper.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace RedditScraper.Services
{
    public class RedditScraperService : IRedditScraperService
    {
        public List<TopScoringReddit> GetAll()
        {
            using (var client = new HttpClient())
            {
                var html = client.GetStringAsync("https://old.reddit.com/r/all/top/").Result;

                var parser = new HtmlParser();

                var document = parser.Parse(html);

                var siteTable = document.QuerySelectorAll("#siteTable > .thing");

                //var nextButton = document.QuerySelectorAll("#siteTable > .nav-buttons > .nextprev > .next-button > a").OfType<IHtmlAnchorElement>();

                var nextButton2 = 1;

                List<TopScoringReddit> results = new List<TopScoringReddit>();

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

                while (nextButton2 < 5)
                {
                    html = client.GetStringAsync("https://www.reddit.com/r/all/top/?count=25").Result;

                    parser = new HtmlParser();

                    document = parser.Parse(html);

                    siteTable = document.QuerySelectorAll("#siteTable > .thing");

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
                    nextButton2++;
                }
                return results;
            }
        }
    }
}