using SticksAndStones.Models.DAL;
using System;
using System.ComponentModel.DataAnnotations;

namespace SticksAndStones.Areas.Admin.Models
{
    public class Banhammer
    {
        [Required]
        public string VictimID { get; set; }

        public User Victim { get; set; }

        [FutureDateValidation]
        [DataType(DataType.DateTime)]
        public DateTime BanExpires { get; set; }

        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}
