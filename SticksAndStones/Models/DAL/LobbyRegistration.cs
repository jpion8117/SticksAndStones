using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SticksAndStones.Models.DAL
{
    [Table("Lobbies")]
    public class LobbyRegistration
    {
        [Key]
        public int LobbyId { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Lobby names are limited to 20 characters.")]
        public string LobbyName { get; set; }

        [NotMapped]
        public bool Joinable { get => GuestId == null && HostId != null; }

        public string HostId { get; set; }
        public virtual User HostUser { get; set; }

        public string GuestId { get; set; }
        public virtual User GuestUser { get; set; }
    }
}
