using Microsoft.AspNetCore.Mvc;
using SticksAndStones.Models.DAL;
using SticksAndStones.Models.GameComponents;
using System.Collections.Generic;
using System.Linq;

namespace SticksAndStones.Controllers
{
    public class PlayController : Controller
    {
        protected PlayerDataContext _playerData;

        public PlayController(PlayerDataContext context)
        {
            _playerData = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(SelectProfile));
        }
        [HttpPost]
        public IActionResult Index(int userID)
        {
            User u = _playerData.Users.Find(userID);
            return View(u);
        }
        public IActionResult SelectProfile()
        {
            List<User> users = _playerData.Users
                .Where(status => !status.IsActive)
                .OrderBy(user => user.UserName)
                .ToList();

            //if no inactive users are available
            if (users.Count == 0)
            {
                return RedirectToAction("Create", "User");
            }

            ViewBag.InactiveProfiles = users;
            return View(new User());
        }
    }
}
