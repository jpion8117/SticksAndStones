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
                    BanMessage = "";
                }
   
                return _banned;
            }
            set
            {
                if (value) //quick ban user
                {
                    _banned = true;
                    _bannedDate = DateTime.Now.AddDays(1);
                    BanMessage = "Temporary ban in place for 1 day.";
                }
                else //unban user
                {
                    _banned = false;
                    _bannedDate = DateTime.Now;
                    BanMessage = "";
                }
            }
        }

        /// <summary>
        /// DateTime when a user's ban will expire. Upon setting property will also set _banned to 
        /// true as long as the date being set is in the future
        /// </summary>
        public DateTime BanDate 
        { 
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
        /// Indicates the reason for a player's ban
        /// </summary>
        public string BanMessage { get; set; }

        /// <summary>
        /// All taglines suggested by this user
        /// </summary>
        [InverseProperty("SuggestedByUser")]
        public virtual ICollection<Tagline> TaglinesSuggested { get; set; }

        /// <summary>
        /// All taglines approved by this user
        /// </summary>
        [InverseProperty("AuthorizedByUser")]
        public virtual ICollection<Tagline> TaglinesAuthorized { get; set; }

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
        public virtual LobbyRegistration HostLobby { get; set; }

        public int? GuestId { get; set; }
        public virtual LobbyRegistration GuestLobby { get; set; }

        public void DropBanhammer(DateTime? banExpires = null, string message = null)
        {
            //quick default 1 day ban
            if (banExpires == null && message == null)
            {
                Banned = true;
            }
            //custom ban date and message set
            else if (banExpires != null && message != null)
            {
                _banned = true;
                _bannedDate = banExpires.Value;
                BanMessage = message;
            }
            //custom ban date no message
            else if (banExpires != null)
            {
                _banned = true;
                _bannedDate = banExpires.Value;
                BanMessage = $"You have been banned for {(_bannedDate - DateTime.Now).TotalDays} days.";
            }
            //custom message standard ban date
            else
            {
                Banned = true;
                BanMessage = message;
            }
        }
    }
}
