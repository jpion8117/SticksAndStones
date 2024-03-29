﻿using SticksAndStones.Models.GameComponents.Characters;

namespace SticksAndStones.Models.GameComponents.Moves.Shared
{
    public class StandardBlock : BaseMove
    {
        private double _originalDefenseMultiplier;
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
        public override string DisplayName => "Block";
        public override string FlavorText => $"Forfeit your turn and block 30% more damage " +
            $"next round who knows that extra time to think may be beneficial to your " +
            $"strategy (recover 1 additional POW) -- 0 POW";

        public StandardBlock(CharacterBase executioner) : base(executioner)
        {
            _moveCost = 0;
            _maxTargets = 1;
            _processModes[ProcessMode.Round] = true; //add process mode for round level event
        }

        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            switch (mode)
            {
                case ProcessMode.Move:
                    _originalDefenseMultiplier = _moveExecutioner.DefenseMultiplier;
                    _moveExecutioner.DefenseMultiplier += 0.3;
                    base.ExecuteCommonAction();
                    _processModes[ProcessMode.Move] = false;
                    _moveExecuted = false;
                    return GameError.SUCCESS;
                case ProcessMode.Round:
                    _moveExecutioner.DefenseMultiplier = _originalDefenseMultiplier;
                    return base.ExecuteCommonAction();
            }

            return GameError.SUCCESS;
        }
    }
}
