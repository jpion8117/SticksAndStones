using SticksAndStones.Models.GameComponents.Characters;

namespace SticksAndStones.Models.GameComponents.Moves.Tank
{
    /// <summary>
    /// Give a party member 10hp at the cost of 15 of your own hp.
    /// </summary>
    public class BloodDonor : BaseMove
    {
        public BloodDonor(CharacterBase executioner) : base(executioner)
        {
            _maxTargets = 1;
            _moveCost = 5;
        }

        public override string Type => "BloodDonorMove";

        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            //check if move is valid
            if (!CheckIfValidMove())
                return GameError.MOVE_INVALID;

            //remove 15 health from executioner
            _moveExecutioner.TakeDamage(15, true);

            //add 10 health to target
            _targets[0].updateHealth(10);

            //run the base version of the ExecuteAction method and return
            return base.ExecuteAction() == GameError.SUCCESS ? GameError.SUCCESS : GameError.IPROCESSABLE_FAILED_EXE;
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
