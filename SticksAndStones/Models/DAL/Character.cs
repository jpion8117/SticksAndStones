using System.Collections.Generic;

namespace SticksAndStones.Models.DAL
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public string Flavortext { get; set; }
        public virtual ICollection<Move> Moves { get; set; }
    }
}
