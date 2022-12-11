using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SticksAndStones.Models.DAL;
using SticksAndStones.Models.ViewModels;
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
                if (_filter.ContainsProfanity(tagline.Content.ToLower()))
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

        public IActionResult ViewTaglines()
        {
            return View();
        }

        public IActionResult CharacterData()
        {
            var viewModel = new FighterInfoViewModel
            {
                CharacterList = _siteData.Characters.ToList(),
                Effects = _siteData.Effects.ToList(),
                Moves = _siteData.Moves.ToList()
            };

            return View(viewModel);
        }

        public IActionResult MoveDetail(int id)
        {
            var move = _siteData.Moves.Find(id);

            if (move == null)
                return NoContent();

            return View(move);
        }

        public IActionResult EffectDetail(int effectId, int returnMoveId)
        {
            var effect = _siteData.Effects.Find(effectId);

            if (effect == null)
                return NoContent();

            ViewBag.ReturnMoveId = returnMoveId;
            ViewBag.ReturnMoveName = _siteData.Moves.Find(returnMoveId).Name;

            return View(effect);
        }
    }
}
