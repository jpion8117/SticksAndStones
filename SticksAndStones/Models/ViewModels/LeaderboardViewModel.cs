using SticksAndStones.Models;
using SticksAndStones.Models.DAL;

namespace SticksAndStones.Models.ViewModels
{
    public class LeaderboardViewModel
    {
        string Filter { get; set; }

        PaginatedList<User> Leaderboard { get; set; }
    }
}
