using System.Collections.Generic;

namespace SticksAndStones.Models.DAL
{
    public class Move
    {
        public int MoveId { get; set; }
        public string Name { get; set; }
        public string Flavortext { get; set; }
        public int? CharacterId { get; set; }
        public virtual Character Character { get; set; }
        public virtual ICollection<MoveEffect> MoveEffects { get; set; }
    }
}
