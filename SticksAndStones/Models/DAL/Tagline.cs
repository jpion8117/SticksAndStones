using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SticksAndStones.Models.DAL
{
    public class Tagline
    {
        public int TaglineId { get; set; }

        [Required]
        public string Content { get; set; }
        public bool Authorized { get; set; }

        public string SuggestedById { get; set; }

        [ForeignKey("SuggestedById")]
        [InverseProperty("TaglinesSuggested")]
        public virtual User SuggestedByUser { get; set; }
        public string AuthorizedById { get; set; }

        [ForeignKey("AuthorizedById")]
        [InverseProperty("TaglinesAuthorized")]
        public virtual User AuthorizedByUser { get; set; }
    }
}
