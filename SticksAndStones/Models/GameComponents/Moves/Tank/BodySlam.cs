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
        public override string DisplayName => "Body Slam";
        public override string FlavorText => $"[Generic large rage monster] shatter! You big… " +
            $"smash puny enemy… Attack an enemy with a simple body slam you seemingly learned " +
            $"from some wrestling show. You know those are scripted right??? Deal 12 damage " +
            $"to one enemy -- {_moveCost} POW";

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
