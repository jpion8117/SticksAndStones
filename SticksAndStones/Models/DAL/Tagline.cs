using System.ComponentModel.DataAnnotations;

namespace SticksAndStones.Models.DAL
{
    public class Tagline
    {
        public int TaglineId { get; set; }

        [Required]
        public string Content { get; set; }
        public bool Authorized { get; set; }
        public string SuggestedById { get; set; }
        public User SuggestedByUser { get; set; }
        public string AuthorizedById { get; set; }
        public User AuthorizedByUser { get; set; }
    }
}
