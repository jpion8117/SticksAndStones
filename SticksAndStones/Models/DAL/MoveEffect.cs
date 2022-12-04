namespace SticksAndStones.Models.DAL
{
    public class MoveEffect
    {
        public int MoveId { get; set; }
        public int EffectId { get; set; }
        public virtual Move Move { get; set; }
        public virtual Effect Effect { get; set; }
    }
}
