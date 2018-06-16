using RedditScraper.Models;
using System.Collections.Generic;

namespace RedditScraper.Services
{
    public interface IRedditScraperService
    {
        List<TopScoringReddit> GetAll();
    }
}