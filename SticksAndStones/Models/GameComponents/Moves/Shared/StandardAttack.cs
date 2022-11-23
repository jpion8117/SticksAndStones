using SticksAndStones.Models.GameComponents.Characters;
using System;

namespace SticksAndStones.Models.GameComponents.Moves.Shared
{
    public class StandardAttack : BaseMove
    {
        private int _attackDamage = 10;
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
        public override string DisplayName => "Basic Attack";
        public override string FlavorText => $"No frills standard attack that’s affected by " +
            $"your attack multiplier. Does minimal damage to one enemy, but you get what " +
            $"you pay for. -- 0 POW";

        public int AttackDamage { get => _attackDamage; }

        public StandardAttack(CharacterBase executioner) : base(executioner)
        {
            _maxTargets = 1;                                                //Standard attacks only effect a single target
            _moveCost = 0;                                                  //Standard attacks have no power cost
            _attackDamage = _attackDamage + 
                (int)Math.Round(_attackDamage * _moveExecutioner.AttackMultiplier, 0);   //determines the move's final attack damage
        }


        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            if (_targets.Count == 0)
                return GameError.MOVE_NO_TARGETS_DEFINED;

            _targets[0].TakeDamage(_attackDamage);

            return GameError.SUCCESS;
        }
    }
}
