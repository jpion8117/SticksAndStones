using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SticksAndStones.Models.DAL
{
    [Table("Lobbies")]
    public class LobbyRegistration
    {
        public int LobbyId { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Lobby names are limited to 20 characters.")]
        public string LobbyName { get; set; }

        public DateTime CreationDate { get; set; }
        
        public string HostId { get; set; }
        public User HostUser { get; set; }
        
        public string GuestId { get; set; }
        public User GuestUser { get; set; }
    }
}
