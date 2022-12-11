using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SticksAndStones.Models.DAL
{
    public class Move
    {
        public int MoveId { get; set; }
        public string Name { get; set; }
        public string Flavortext { get; set; }
        public int? CharacterId { get; set; }
        public virtual Character Character { get; set; }
        [DisplayName("Effects")]
        public virtual ICollection<MoveEffect> MoveEffects { get; set; }
        public bool ContainsEffect(int effectID)
        {
            foreach (var effect in MoveEffects)
            {
                if (effect.EffectId == effectID)
                    return true;
            }

            return false;
        }
        public bool ContainsEffect(int effectID, List<MoveEffect> moveEffects)
        {
            foreach (var effect in moveEffects)
            {
                if (effect.EffectId == effectID)
                    return true;
            }

            return false;
        }
    }
}
