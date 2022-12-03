namespace SticksAndStones.Models.DAL
{
    public class MoveEffect
    {
        public int MoveId { get; set; }
        public int EffectId { get; set; }
        public Move Move { get; set; }
        public Effect Effect { get; set; }
    }
}
