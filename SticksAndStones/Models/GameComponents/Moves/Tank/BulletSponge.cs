using SticksAndStones.Models.GameComponents.Characters;

namespace SticksAndStones.Models.GameComponents.Moves.Tank
{ /// <summary>
  /// Absorb all party damage for one round.
  /// </summary>
    public class BulletSponge : BaseMove
    {
        public BulletSponge(CharacterBase executioner) : base(executioner)
        {
            _maxTargets = 4;
            _moveCost = 5;
        }

        public override string Type => "BulletSpongeMove";

        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            //check if move is valid
            if (!CheckIfValidMove())
                return GameError.MOVE_INVALID;

            //set the redirects to the executioner
            for (int i = 0; i < _targets.Count; i++)
            {
                _targets[i].SetRedirect(_moveExecutioner);
            }

            //run the base version of the ExecuteAction method and return
            return base.ExecuteAction() == GameError.SUCCESS ? GameError.SUCCESS : GameError.IPROCESSABLE_FAILED_EXE;
        }

        public override bool CheckIfValidMove()
        {
            foreach (CharacterBase target in _targets)
            {
                //validate all targets are on the same team
                if (target.PartyID != _moveExecutioner.PartyID)
                    return false;

                //validate to make sure executioner is not targeting self
                else if (target.UniqueID == _moveExecutioner.UniqueID)
                    return false;
            }

            return base.CheckIfValidMove();
        }
    }
}
