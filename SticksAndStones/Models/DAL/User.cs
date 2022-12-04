using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SticksAndStones.Models.DAL
{
    public class User : IdentityUser
    {
        private bool _banned;
        private DateTime _bannedDate;

        /// <summary>
        /// Identifies the total number of games a user has played.
        /// </summary>
        public int GamesPlayed { get; set; }

        /// <summary>
        /// Identifies how many games the user has won
        /// </summary>
        public int GamesWon { get; set; }

        /// <summary>
        /// Identifies how many games the user has lost
        /// </summary>
        [NotMapped]
        public int GamesLost
        {
            get => GamesPlayed - GamesWon;
        }

        /// <summary>
        /// Represents the percentage of games a player has won.
        /// </summary>
        [NotMapped]
        public string WinRate
        {
            get => ((float)GamesWon / (float)GamesPlayed).ToString("P");
        }

        /***************************************************************************************
         * 
         * Everything below is proof of concept and may not be used but seemed important 
         * to include and also fun to implement.
         * 
         **************************************************************************************/

        /// <summary>
        /// Indicates a player has been banned from playing for a given time. When set through this property ban lasts for 1 day.
        /// Automatically repeals any bans that have expired when checked.
        /// </summary>
        public bool Banned 
        { 
            get
            {
                // unbans user if they are due to be unbanned.
                if (_banned == true && _bannedDate < DateTime.Now)
                {
                    _banned = false;
                    _bannedDate = DateTime.Now;
                }
   
                return _banned;
            }
            set
            {
                if (value) //quick ban user
                {
                    _banned = true;
                    _bannedDate = DateTime.Now.AddDays(1);
                }
                else //unban user
                {
                    _banned = false;
                    _bannedDate = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// DateTime when a user's ban will expire. Upon setting property will also set _banned to 
        /// true as long as the date being set is in the future
        /// </summary>
        public DateTime BanDate { 
            get => _bannedDate; 
            set
            {
                if (value > DateTime.Now)
                {
                    _bannedDate = value;
                    _banned = true;
                }
            }
        }

        /// <summary>
        /// All taglines suggested by this user
        /// </summary>
        [InverseProperty("SuggestedByUser")]
        public ICollection<Tagline> TaglinesSuggested { get; set; }

        /// <summary>
        /// All taglines approved by this user
        /// </summary>
        [InverseProperty("AuthorizedByUser")]
        public ICollection<Tagline> TaglinesAuthorized { get; set; }

        [NotMapped]
        public LobbyRegistration CurrentLobby
        {
            get
            {
                if (HostId != null)
                {
                    return HostLobby;
                }
                else if (GuestId != null)
                {
                    return GuestLobby;
                }

                return null;
            }
        }

        public int? HostId { get; set; }
        public LobbyRegistration HostLobby { get; set; }

        public int? GuestId { get; set; }
        public LobbyRegistration GuestLobby { get; set; }
    }
}
