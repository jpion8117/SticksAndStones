﻿using SticksAndStones.Models.GameComponents.Characters;

namespace SticksAndStones.Models.GameComponents.Moves.Tank
{
    /// <summary>
    /// Give a party member 10hp at the cost of 15 of your own hp.
    /// </summary>
    public class BloodDonor : BaseMove
    {
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
        public override string DisplayName => "Blood Donor";
        public override string FlavorText => $"A friend in need… Give one party member the " +
            $"extra boost they need by donating blood today! Heal a party member 10 HP at " +
            $"the expense of 15 of your own HP (sorry, we spilled some in " +
            $"transit) -- {_moveCost} POW";

        public BloodDonor(CharacterBase executioner) : base(executioner)
        {
            _maxTargets = 1;
            _moveCost = 5;
        }


        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            //check if move is valid
            if (!CheckIfValidMove())
                return GameError.MOVE_INVALID;

            //remove 15 health from executioner
            _moveExecutioner.TakeDamage(15, true);

            //add 10 health to target
            _targets[0].UpdateHealth(10);

            //run the base version of the ExecuteAction method and return
            return base.ExecuteCommonAction() == GameError.SUCCESS ? GameError.SUCCESS : GameError.IPROCESSABLE_FAILED_EXE;
        }

        public override bool CheckIfValidMove()
        {
            bool baseValidationCheck = base.CheckIfValidMove();

            if (baseValidationCheck)
            {
                //check that player is not targeting self and is targeting alive party member
                if (_targets[0].PartyID != _moveExecutioner.PartyID || !_targets[0].IsAlive ||
                    _targets[0].UniqueID == _moveExecutioner.UniqueID)
                    return false;
            }

            return baseValidationCheck;
        }
    }
}
