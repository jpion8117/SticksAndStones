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

        /// <summary>
        /// Name given to this lobby by the host user that created it.
        /// </summary>
        [Required]
        [MaxLength(20, ErrorMessage = "Lobby names are limited to 20 characters.")]
        public string LobbyName { get; set; }

        /// <summary>
        /// Boolean value that checks if lobby is joinable, read only, not saved in 
        /// database.
        /// </summary>
        [NotMapped]
        public bool Joinable { get => GuestId == null && HostId != null; }

        /// <summary>
        /// Distinguishes games that have been concluded from active games.
        /// </summary>
        public bool Inactive { get; set; }

        /// <summary>
        /// Indicates a lobby that has been set to private by the host. A private lobby can only be joined
        /// via a link shared by the host player and will not show up in the lobby search!
        /// </summary>
        public bool Private { get; set; }

        /// <summary>
        /// Player hosting this lobby (Id)
        /// </summary>
        public string HostId { get; set; }
        /// <summary>
        /// Player hosting this lobby
        /// </summary>
        public virtual User Host { get; set; }

        /// <summary>
        /// Player joining this lobby (Id)
        /// </summary>
        public string GuestId { get; set; }
        /// <summary>
        /// Player joining this lobby
        /// </summary>
        public virtual User Guest { get; set; }

        /// <summary>
        /// Player who won the lobby (id)
        /// </summary>
        public string WinnerId  { get; set; }
        /// <summary>
        /// Player who won the lobby
        /// </summary>
        public virtual User Winner { get; set; }

        /// <summary>
        /// DateTime a winner was declared
        /// </summary>
        public DateTime? GameComplete { get; set; }
    }
}
