using SticksAndStones.Models.GameComponents.Characters;

namespace SticksAndStones.Models.GameComponents.Moves.Tank
{
    /// <summary>
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
            if(!CheckIfValidMove())
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

            if(baseValidationCheck)
            {
                //check that player is not targeting self and is targeting alive party member
                if (_targets[0].PartyID != _moveExecutioner.PartyID || !_targets[0].IsAlive || 
                    _targets[0].UniqueID == _moveExecutioner.UniqueID)
                        return false;
            }

            return baseValidationCheck;
        }
    }
    /// <summary>
    /// Attack an enemy with a move dealing 12hp.
    /// </summary>
    public class BodySlam : BaseMove
    {
        private int _attackDamage;
        public BodySlam(CharacterBase executioner) : base(executioner)
        {
            _moveCost = 5;
            _maxTargets = 1;
            _attackDamage = GetMoveBaseDamage(12);
        }

        public override string Type => "BodySlamMove";

        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            if (!CheckIfValidMove())
                return GameError.MOVE_INVALID;
            var damage = GetMoveAdjustedDamage(_attackDamage);
            _targets[0].TakeDamage(damage);

            return base.ExecuteAction();
        }
    }
    /// <summary>
    /// Increase defense by 1.5 and absorb attack from one other party member
    /// </summary>
    public class ManOfSteelSpecial : BaseMove
    {
        private bool _moveRun = false;
        public ManOfSteelSpecial(CharacterBase executioner) : base(executioner)
        {
            _maxTargets = 1;
            _moveCost = 16;
            _processModes[ProcessMode.Round] = true;
        }

        public override string Type => "ManOfSteelSpecialMove";

        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            if (!CheckIfValidMove())
                return GameError.MOVE_INVALID;

            switch (mode)
            {
                case ProcessMode.Move:
                    if (!_moveRun)
                    {
                        //double executioner's defense
                        _moveExecutioner.DefenseMultiplier = 0.9f;

                        //redirect next attack to executioner
                        _targets[0].SetRedirect(_moveExecutioner);

                        if (base.ExecuteAction() == GameError.SUCCESS)
                        {
                            //keep move in queue to reset defense at round level
                            _moveExecuted = false;

                            return GameError.SUCCESS;
                        }
                        else
                            return GameError.MOVE_INVALID;
                    }
                    return GameError.SUCCESS;

                case ProcessMode.Round:
                    //return defense to original value
                    _moveExecutioner.DefenseMultiplier = 0.5f;

                    return GameError.SUCCESS;
            }

            return GameError.SUCCESS;
        }
    }
}
