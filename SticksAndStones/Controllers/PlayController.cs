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
            return View("ComingSoon");
        }

        [HttpGet]
        public IActionResult Tutorial()
        {
            return View("ComingSoon");
        }

         /*********************************************************
         *                                                        *
         *               Game Interaction Actions                 *
         *                                                        *
         *********************************************************/
        
        [HttpPost]
        public JsonResult GetUserInfo(int id)
        {
            User user = _playerData.Users.Find(id);
            return Json(user);
        }
        /// <summary>
        /// Client systems must periodically check in with the server to ensure they are still 
        /// connected to the game. This method takes the lobbyID of the lobby the player is 
        /// checking into and notes the time of the check in. It also verifies the opponent 
        /// has not disconnected. Finally, informs a player if they have been disconnected.
        /// </summary>
        /// <param name="lobbyID">Id of the lobby to be pinged</param>
        /// <returns>Json containing information about the state of the lobby</returns>
        [HttpPost]
        public JsonResult PingLobby(int lobbyID)
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
