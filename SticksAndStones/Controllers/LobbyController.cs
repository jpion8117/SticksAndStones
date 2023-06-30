using Microsoft.AspNetCore.Mvc;

namespace SticksAndStones.Controllers
{
    public class LobbyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
