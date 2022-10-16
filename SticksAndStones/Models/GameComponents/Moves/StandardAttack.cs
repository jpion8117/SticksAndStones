using SticksAndStones.Models.GameComponents.Characters;

namespace SticksAndStones.Models.GameComponents.Moves
{
    public class StandardAttack : BaseMove
    {
        private int _attackDamage = -5;
        public StandardAttack(CharacterBase executioner) : base(executioner)
        {
            _maxTargets = 1;                                                //Standard attacks only effect a single target
            _moveCost = 0;                                                  //Standard attacks have no power cost
            _attackDamage = _attackDamage + 
                (int)(_attackDamage * _moveExecutioner.AttackMultiplier);   //determines the move's final attack damage
        }

        public override string Type { get { return "Attack!"; } }

        public override GameError ExecuteAction()
        {
            if (_targets.Count == 0)
                return GameError.MOVE_NO_TARGETS_DEFINED;

            _targets[0].updateHealth(_attackDamage);

            return GameError.SUCCESS;
        }
    }
}
