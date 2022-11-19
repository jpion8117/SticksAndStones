using SticksAndStones.Models.GameComponents.Characters;

namespace SticksAndStones.Models.GameComponents.Moves.Tank
{
    /// <summary>
    /// Attack an enemy with a move dealing 12hp.
    /// </summary>
    public class BodySlam : BaseMove
    {
        private int _attackDamage;
        public BodySlam(CharacterBase executioner) : base(executioner)
        {
            _moveCost = 5;
            _maxTargets = 1;
            _attackDamage = GetMoveBaseDamage(12);
        }

        public override string Type => "BodySlamMove";

        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            if (!CheckIfValidMove())
                return GameError.MOVE_INVALID;
            var damage = GetMoveAdjustedDamage(_attackDamage);
            _targets[0].TakeDamage(damage);

            return base.ExecuteAction();
        }
    }
}
