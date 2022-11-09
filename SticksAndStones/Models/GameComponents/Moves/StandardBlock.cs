using SticksAndStones.Models.GameComponents.Characters;

namespace SticksAndStones.Models.GameComponents.Moves
{
    public class StandardBlock : BaseMove
    {
        private double _originalDefenseMultiplier;
        private int _runCount = 0;
        public StandardBlock(CharacterBase executioner) : base(executioner)
        {
            _moveCost = 0;
            _maxTargets = 1;
        }

        public override string Type => "StandardBlockMove";

        public override GameError ExecuteAction()
        {
            if (_runCount == 0)
            {
                _originalDefenseMultiplier = _moveExecutioner.DefenseMultiplier;
                _moveExecutioner.DefenseMultiplier *= 1.25f;
                base.ExecuteAction();
                _moveExecuted = false;
                return GameError.SUCCESS;
            }

            _moveExecutioner.DefenseMultiplier = _originalDefenseMultiplier;
            return base.ExecuteAction();
        }
    }
}
