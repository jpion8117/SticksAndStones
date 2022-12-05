using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SticksAndStones.Models.DAL;
using System.Linq;

namespace SticksAndStones.Controllers
{
    public class GameInfo : Controller
    {
        private SiteDataContext _siteData;
        private ProfanityFilter.ProfanityFilter _filter = new ProfanityFilter.ProfanityFilter();

        public GameInfo(SiteDataContext siteData)
        {
            _siteData = siteData;
        }

        /// <summary>
        /// Redirecting "/GameInfo/" to "/Home/About/" because that makes the most sense
        /// </summary>
        public IActionResult Index()
        {
            return RedirectToAction("About", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult SubmitTagline()
        {
            var user = _siteData.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            ViewBag.UserId = user.Id;

            return View(new Tagline());
        }
        [HttpPost]
        [Authorize]
        public IActionResult SubmitTagLine(Tagline tagline)
        {
            if (ModelState.IsValid)
            {
                if (_filter.ContainsProfanity(tagline.Content))
                {
                    ModelState.AddModelError("Content", "Swearing is not aloud, please revise your submition.");
                    return View(tagline);
                }

                _siteData.Taglines.Add(tagline);
                _siteData.SaveChanges();
                return RedirectToActionPermanent("Index", "Home");
            }

            return View(tagline);
        }
    }
}
