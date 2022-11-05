using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SticksAndStones.Models.DAL
{
    public class User
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Username must not exceed 15 characters.")]
        [RegularExpression("([a-z]|[A-Z]|[0-3]|_|-|)", ErrorMessage = "User name may only contain letters, number, or " +
            "the '_' and '-' characters")]
        public string Username { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get { return GamesPlayed - GamesWon; } }
        public double WinRate 
        { 
            get 
            {
                if (GamesPlayed == 0) return 0;
                return Math.Round(((double)GamesWon / (double)GamesPlayed) * 100, 2); 
            } 
        }
        [Required]
        public bool IsActive { get; set; }
    }
}
