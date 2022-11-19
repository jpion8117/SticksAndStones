using SticksAndStones.Models.GameComponents.Characters;
using System;

namespace SticksAndStones.Models.GameComponents.Moves.Shared
{
    public class StandardAttack : BaseMove
    {
        private int _attackDamage = 10;
        public int AttackDamage { get { return _attackDamage; } }
        public StandardAttack(CharacterBase executioner) : base(executioner)
        {
            _maxTargets = 1;                                                //Standard attacks only effect a single target
            _moveCost = 0;                                                  //Standard attacks have no power cost
            _attackDamage = _attackDamage + 
                (int)Math.Round(_attackDamage * _moveExecutioner.AttackMultiplier, 0);   //determines the move's final attack damage
        }

        public override string Type { get { return "Attack!"; } }

        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            if (_targets.Count == 0)
                return GameError.MOVE_NO_TARGETS_DEFINED;

            _targets[0].TakeDamage(_attackDamage);

            return GameError.SUCCESS;
        }
    }
}
