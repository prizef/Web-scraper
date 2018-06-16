using RedditScraper.Models;
using RedditScraper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RedditScraper.Controllers
{
    public class RedditScraperController : ApiController
    {
        readonly IRedditScraperService redditScraperService;

        public RedditScraperController(IRedditScraperService redditScraperService)
        {
            this.redditScraperService = redditScraperService;
        }

        [Route("api/reddit"), HttpGet]
        public HttpResponseMessage GetAll()
        {
            var results = redditScraperService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }
    }
}
