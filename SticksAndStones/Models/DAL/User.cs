﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SticksAndStones.Models.DAL
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
