using SticksAndStones.Models.GameComponents.Characters;

namespace SticksAndStones.Models.GameComponents.Moves.Tank
{
    /// <summary>
    /// Increase defense by 1.5 and absorb attack from one other party member
    /// </summary>
    public class ManOfSteelSpecial : BaseMove
    {
        private bool _moveRun = false;
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

        public ManOfSteelSpecial(CharacterBase executioner) : base(executioner)
        {
            _maxTargets = 1;
            _moveCost = 16;
            _processModes[ProcessMode.Round] = true;
        }

        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            switch (mode)
            {
                case ProcessMode.Move:
                    if (!CheckIfValidMove())
                        return GameError.MOVE_INVALID;
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
                    Completed = true;
                    return GameError.SUCCESS;
            }

            return GameError.SUCCESS;
        }
    }
}
