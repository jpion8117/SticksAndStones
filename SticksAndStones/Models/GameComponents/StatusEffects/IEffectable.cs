namespace SticksAndStones.Models.GameComponents.StatusEffects
{
    /// <summary>
    /// Used as an abstract way of interfacing with objects that can have status effects
    /// applied to them. Primarily players, however, could serve as a way to effect any 
    /// fututre object.
    /// </summary>
    public interface IEffectable
    {
        /// <summary>
        /// Used to update the health of an effectable entitiy
        /// </summary>
        /// <param name="changeAmount">How much you are changing the health level (EX: 
        /// -10 would attempt to remove 10pts of health<</param>
        public GameError updateHealth(int changeAmount);

        /// <summary>
        /// Used to update the power of an effectable entity
        /// </summary>
        /// <param name="changeAmount">How much you are changing the power level (EX: 
        /// -10 would attempt to remove 10pts of power</param>
        public GameError updatePower(int changeAmount);

        /// <summary>
        /// Used to update the defense multiplier of an entity. This can either
        /// buff or debuff defense from -1 (-100%) to 1 (100%) with 0 negating 
        /// any multiplier at all.
        /// </summary>
        /// <param name="changeAmount">Percent you want to change the multiplier
        /// expressed as a decimal between -1 and 1</param>
        public GameError updateDefense (float changeAmount);

        /// <summary>
        /// Used to update the attack multiplier of an entity. This can either
        /// buff or debuff attack from -1 (-100%) to 1 (100%) with 0 negating 
        /// any multiplier at all.
        /// </summary>
        /// <param name="changeAmount">Percent you want to change the multiplier
        /// expressed as a decimal between -1 and 1</param>
        public GameError updateAttack (float changeAmount);

        /// <summary>
        /// Adds status effect to an effectable entity.
        /// </summary>
        /// <param name="effect">effect to add</param>
        public GameError addEffect(BaseStatusEffect effect);

        /// <summary>
        /// Removes status effect on an effectable entity.
        /// </summary>
        /// <param name="effect">effect to remove</param>
        public GameError removeEffect(BaseStatusEffect effect);

        /// <summary>
        /// Purges any status effects that have the IsNegative property set 
        /// to true.
        /// </summary>
        /// <returns></returns>
        public GameError purgeNegativeEffects();
    }
}
