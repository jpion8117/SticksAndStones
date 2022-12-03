using Microsoft.AspNetCore.Mvc;
using SticksAndStones.Models.DAL;
using SticksAndStones.Models.GameComponents;
using System.Collections.Generic;
using System.Linq;

namespace SticksAndStones.Controllers
{
    public class PlayController : Controller
    {
        protected SiteDataContext _playerData;

        public PlayController(SiteDataContext context)
        {
            _playerData = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(SelectLobby));
        }
        [HttpPost]
        public IActionResult Index(int userID)
        {
            User u = _playerData.Users.Find(userID);
            return View(u);
        }
       
        public IActionResult SelectLobby(int userID)
        {
            //get the user profile from the database
            User user = _playerData.Users.Find(userID);

            return View(user);
        }
        [HttpPost]
        public IActionResult SticksAndStones(int lobbyID)
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
        public JsonResult CheckIn(int lobbyID)
        {
            Lobby lobby = Lobby.GetLobbyByID(lobbyID);
            return Json(lobby);
        }
        [HttpPost]
        public JsonResult GetLobbyInfo(int lobbyID)
        {
            return Json(Lobby.GetLobbyByID(lobbyID));
        }
    }
}
