using System.ComponentModel.DataAnnotations;

namespace SticksAndStones.Models
{
    public class User
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get { return GamesPlayed - GamesWon; } }
        public double WinRate { get { return ((double)GamesWon / (double)GamesPlayed) * 100; } }
    }
}
