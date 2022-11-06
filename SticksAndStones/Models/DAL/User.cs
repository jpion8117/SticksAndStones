using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace SticksAndStones.Models.DAL
{
    public class User
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Username must not exceed 15 characters.")]
        [RegularExpression("([a-z]|[A-Z]|[0-9]|[_-][^ ]){0,30}", ErrorMessage = "User name may only contain letters, number, or " +
            "the '_' and '-' characters")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Display(Name = "Real Name")]
        [Column("Name")]
        [StringLength(50, ErrorMessage = "Real name may not exceede 50 characters.")]
        [RegularExpression("([a-z]|[A-Z]){3,24} {1}([a-z]|[A-Z]){3,24}", ErrorMessage = "Please provide first and last name " +
            "separated by a space.")]
        public string RealName { get; set; }

        [Display(Name = "Games Played")]
        [Range(0,9999999)]
        public int GamesPlayed { get; set; }

        [Display(Name = "Games Won")]
        [Range(0,9999999)]
        public int GamesWon { get; set; }

        [Display(Name = "Games Lost")]
        public int GamesLost { get { return GamesPlayed - GamesWon; } }

        [Display(Name = "Win Rate")]
        public double WinRate 
        { 
            get 
            {
                if (GamesPlayed == 0) return 0;
                return (double)GamesWon / (double)GamesPlayed; 
            } 
        }
        [Required]
        public bool IsActive { get; set; }
    }
}
