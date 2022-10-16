using System;
using System.ComponentModel.DataAnnotations;

namespace SticksAndStones.Models.DAL
{
    public class Leaderboard
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public uint Ranking { get; set; }
    }
}
