using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SticksAndStones.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SticksAndStones.Models.DAL;
using Microsoft.AspNetCore.Http;

namespace SticksAndStones.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SiteDataContext _siteData;

        public HomeController(ILogger<HomeController> logger, SiteDataContext siteData)
        {
            _logger = logger;
            _siteData = siteData;
        }

        public IActionResult Index()
        {
            if (!_siteData.Taglines.Where(tl => tl.Authorized).Any())
            {
                ViewBag.tagLine = "Sticks and stones will break my bones and these words will hurt you too...";
            }
            else
            {
                Random r = new Random();
                var tagline = _siteData.Taglines.Where(tl => tl.Authorized).Skip(r.Next(0, _siteData.Taglines.Where(tl => tl.Authorized).Count())).FirstOrDefault();
                ViewBag.Tagline = $"{tagline.Content} - {tagline.SuggestedByUser.UserName}";
            }

            if (HttpContext.Session.GetString("RedirectHome") == null)
            {
                HttpContext.Session.SetString("RedirectHome", "true");
                return View();
            }

            return View("HomePage");
        }
        [Route("GameInfo")]
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
