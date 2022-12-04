using Microsoft.AspNetCore.Mvc;

namespace SticksAndStones.Controllers
{
    public class GameInfo : Controller
    {
        private ProfanityFilter.ProfanityFilter _filter = new ProfanityFilter.ProfanityFilter();

        /// <summary>
        /// Redirecting "/GameInfo/" to "/Home/About/" because that makes the most sense
        /// </summary>
        public IActionResult Index()
        {
            return RedirectToAction("About", "Home");
        }


    }
}
