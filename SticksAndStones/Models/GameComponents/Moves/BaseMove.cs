using SticksAndStones.Models.GameComponents;
using System.Collections.Generic;

namespace SticksAndStones.Models.Moves.GameComponents
{
    abstract public class BaseMove : IProcessable
    {
        protected List<Player> _targets; //all players that will be effected by this move
        protected int _maxTargets; //the number of players this move can effect
        protected int _moveCost; //how much power the move consumes
        protected Player _moveExecutioner; //defines the player who is performing the move
        protected ulong _uID;
        protected bool _moveExecuted = false;

        /// <summary>
        /// Adds a target to the move's targets list up to the max number of targets
        /// </summary>
        /// <param name="target">Target player you are attempting to add</param>
        /// <returns>Either a success code or an error that can get fed back to user or system</returns>
        virtual public GameError AddTarget(Player target)
        {
            //check if player has already been targeted
            foreach (var player in _targets)
            {
                //if match is found return duplicate target error
                if (player.UniqueID == target.UniqueID)
                    return GameError.MOVE_TARGET_DUPLICATE;
            }

            //check if there is room for target, add to list if true, return 
            //max targets exceeded error if false
            if (_targets.Count < _maxTargets)
                _targets.Add(target);
            else
                return GameError.MOVE_MAX_TARGETS_EXCEEDED;

            //if code execution reaches this line, no errors were discovered and success
            //code can be sent
            return GameError.SUCCESS;
        }
        /// <summary>
        /// Removes a target to the move's target list if they are already targeted
        /// </summary>
        /// <param name="target">Target to be removed</param>
        /// <returns>Either a success code or an error that can get fed back to user or system</returns>
        virtual public GameError RemoveTarget(Player target)
        {
            //check if targets list is empty
            if (_targets.Count == 0)
            {
                return GameError.MOVE_NO_TARGETS_DEFINED;
            }

            //search targets list for target
            for (int index = 0; index < _targets.Count; index++)
            {
                //if target is found remove target and return success code
                if (target.UniqueID == _targets[index].UniqueID)
                {
                    _targets.RemoveAt(index);
                    return GameError.SUCCESS;
                }
            }

            //target wasn't found
            return GameError.MOVE_TARGET_NOT_FOUND;
        }

        abstract public GameError ExecuteAction();

        /// <summary>
        /// Indicates if the move is valid or not
        /// </summary>
        virtual public bool ValidMove
        {
            get { return (_moveExecutioner.Power - _moveCost >= 0); }
        }

        public int Priority { get { return 1; } }

        public ulong UniqueID { get { return _uID; } }

        public object IdentifiableObject { get { return this; } }
        public bool Completed
        {
            get { return _moveExecuted; }
            set { _moveExecuted = value; }
        }

        abstract public string Type { get; }


    }
}
