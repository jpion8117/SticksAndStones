using SticksAndStones.Models.GameComponents.Characters;

namespace SticksAndStones.Models.GameComponents.Moves.Tank
{
    /// <summary>
    /// Attack an enemy with a move dealing 12hp.
    /// </summary>
    public class BodySlam : BaseMove
    {
        private int _attackDamage;
        private int _moveCost;
        private int _maxTargets;

        public override int MoveCost
        {
            get => _moveCost;
            protected set => _moveCost = value;
        }
        public override int MaxTargets
        {
            get => _maxTargets;
            protected set => _maxTargets = value;
        }

        public BodySlam(CharacterBase executioner) : base(executioner)
        {
            _moveCost = 5;
            _maxTargets = 1;
            _attackDamage = GetMoveBaseDamage(12);
        }

        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            if (!CheckIfValidMove())
                return GameError.MOVE_INVALID;
            var damage = GetMoveAdjustedDamage(_attackDamage);
            _targets[0].TakeDamage(damage);

            return base.ExecuteCommonAction();
        }
    }
}
