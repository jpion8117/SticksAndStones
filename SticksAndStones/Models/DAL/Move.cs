using System.Collections.Generic;

namespace SticksAndStones.Models.DAL
{
    public class Move
    {
        public int MoveId { get; set; }
        public string Name { get; set; }
        public string Flavortext { get; set; }
        public int? CharacterId { get; set; }
        public Character Character { get; set; }
        public ICollection<MoveEffect> MoveEffects { get; set; }
    }
}
