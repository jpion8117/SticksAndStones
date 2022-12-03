using System.Collections.Generic;

namespace SticksAndStones.Models.DAL
{
    public class Effect
    {
        public int EffectId { get; set; }
        public string Name { get; set; }
        public string Flavortext { get; set; }
        public ICollection<MoveEffect> MoveEffects { get; set; }
    }
}
