using SticksAndStones.Models.GameComponents.Characters;

namespace SticksAndStones.Models.GameComponents.Moves
{
    public class StandardBlock : BaseMove
    {
        private double _originalDefenseMultiplier;
        public StandardBlock(CharacterBase executioner) : base(executioner)
        {
            _moveCost = 0;
            _maxTargets = 1;
            _processModes[ProcessMode.Round] = true; //add process mode for round level event
        }

        public override string Type => "StandardBlockMove";

        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            switch (mode)
            {
                case ProcessMode.Move:
                    _originalDefenseMultiplier = _moveExecutioner.DefenseMultiplier;
                    _moveExecutioner.DefenseMultiplier += 0.3;
                    base.ExecuteAction();
                    _processModes[ProcessMode.Move] = false;
                    _moveExecuted = false;
                    return GameError.SUCCESS;
                case ProcessMode.Round:
                    _moveExecutioner.DefenseMultiplier = _originalDefenseMultiplier;
                    return base.ExecuteAction();
            }

            return GameError.SUCCESS;
        }
    }
}
