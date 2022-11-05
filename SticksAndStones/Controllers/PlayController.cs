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
                .OrderBy(user => user.Username)
                .ToList();

            //if no inactive users are available
            if (users.Count == 0)
            {
                return RedirectToAction("Create", "User");
            }

            ViewBag.InactiveProfiles = users;
            return View(new User());
        }
        public IActionResult SelectLobby(int userID)
        {
            //get the user profile from the database
            User user = _playerData.Users.Find(userID);

            //check if user wasn't found or if user is already in use
            if (user == null || user.IsActive)
            {
                return RedirectToAction("SelectProfile", "Play");
            }

            return View(user);
        }
        [HttpPost]
        public IActionResult SticksAndStones(ulong lobbyID)
        {
            Lobby lobby = Lobby.GetLobbyByID(lobbyID);
            return View(lobby);
        }
        [HttpPost]
        public JsonResult GetUserInfo(int id)
        {
            User user = _playerData.Users.Find(id);
            return Json(user);
        }
        [HttpPost]
        public void LockUser(int id)
        {
            User u = _playerData.Users.Find(id);
            u.IsActive = true;
            _playerData.Users.Update(u);
            _playerData.SaveChanges();
        }
        [HttpPost]
        public void unlockUser(int id)
        {
            User u = _playerData.Users.Find(id);
            u.IsActive = false;
            _playerData.Users.Update(u);
            _playerData.SaveChanges();
        }
        [HttpPost]
        public JsonResult CheckIn(ulong lobbyID)
        {
            Lobby lobby = Lobby.GetLobbyByID(lobbyID);
            return Json(lobby);
        }
        [HttpPost]
        public JsonResult GetLobbyInfo(ulong lobbyID)
        {
            return Json(Lobby.GetLobbyByID(lobbyID));
        }
    }
}
