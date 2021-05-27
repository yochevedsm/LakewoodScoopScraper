using LakewoodScoopScraper.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LakewoodScoopScaper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScraperController : ControllerBase
    {
        [Route("scrape")]
        public List<LSElement> Scrape(string query)
        {
            return Scraper.ScrapeLakewoodScoop();
        }
    }
}
