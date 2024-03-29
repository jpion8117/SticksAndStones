﻿using SticksAndStones.Models.GameComponents;
using SticksAndStones.Models.GameComponents.Characters;
using System;
using System.Collections.Generic;

namespace SticksAndStones.Models.GameComponents.Moves
{
    abstract public class BaseMove : IProcessable
    {
        protected List<CharacterBase> _targets; //all players that will be effected by this move
        //protected int _maxTargets; //the number of players this move can effect
        //protected int _moveCost; //how much power the move consumes
        protected CharacterBase _moveExecutioner; //defines the player who is performing the move
        protected ulong _uID;
        protected bool _moveExecuted = false;
        protected Dictionary<ProcessMode, bool> _processModes = new Dictionary<ProcessMode, bool>();

        public BaseMove(CharacterBase executioner)
        {
            _uID = UniqueIDGenerator.GetID(this);
            _moveExecutioner = executioner;
            _targets = new List<CharacterBase>();
            _processModes = new Dictionary<ProcessMode, bool>();
        }

        /// <summary>
        /// Adds a target to the move's targets list up to the max number of targets
        /// </summary>
        /// <param name="target">Target player you are attempting to add</param>
        /// <returns>Either a success code or an error that can get fed back to user or system</returns>
        virtual public GameError AddTarget(CharacterBase target)
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
            if (_targets.Count < MaxTargets)
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
        virtual public GameError RemoveTarget(CharacterBase target)
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

        /// <summary>
        /// Checks to see if a character has enough power to perform a requested move
        /// </summary>
        /// <param name="executioner">Character performing move</param>
        /// <returns>True if character has enough power to perform move</returns>
        public virtual bool CheckIfValidMove()
        { 
            return _moveExecutioner.Power - MoveCost >= 0 && _targets.Count > 0 && _targets.Count <= MaxTargets; 
        }

        /// <summary>
        /// *** Inherited from the IProcessable interface *** Executes any actions of the move on the
        /// specified targets upon exicution, this can be anything from healing allies to damaging 
        /// enemies.
        /// </summary>
        /// <returns>GameError code indicating either success or an error occured</returns>
        public abstract GameError ExecuteAction(ProcessMode mode = ProcessMode.Move);

        protected virtual GameError ExecuteCommonAction()
        {
            //deduct move cost from executioner power level
            _moveExecutioner.UpdatePower(-MoveCost);

            //mark move as complete
            _moveExecuted = true;

            return GameError.SUCCESS;
        }

        /// <summary>
        /// *** Inherited from IProcessable interface *** Defines the priority a processable entity will
        /// exicute at, the higher the number the lower the priority. Note, this should be defined as a 
        /// fixed value for each object type with moves exicuting with a priority of 1 as they must execute
        /// first.
        /// </summary>
        public int Priority { get { return 1; } }
        
        /// <summary>
        /// *** Inherited from the IProcessable interface *** Used to indicate an entitiy can be purged from 
        /// an IProcessable queue.
        /// </summary>
        public bool Completed
        {
            get { return _moveExecuted; }
            set { _moveExecuted = value; }
        }

        /// <summary>
        /// How much power this move uses to execute
        /// </summary>
        public abstract int MoveCost { get; protected set; }
        public abstract int MaxTargets { get; protected set; }

        public abstract string DisplayName { get; }
        public abstract string FlavorText { get; }

        /// <summary>
        /// *** Inherited from the IIdentifiable interface *** Retrieves the identifiable object's unique
        /// runtime ID.
        /// </summary>
        public ulong UniqueID { get { return _uID; } }

        public Dictionary<ProcessMode, bool> ProcessModesUsed => _processModes;

        /// <summary>
        /// calculate the move's base damage from an adjusted target based on a given damage multiplier.
        /// </summary>
        /// <param name="targetDamage"></param>
        /// <returns></returns>
        protected int GetMoveBaseDamage(int targetDamage)
        {
            return (int)(targetDamage / (Math.Round(_moveExecutioner.AttackMultiplier, 2) + 1));
        }
        protected int GetMoveAdjustedDamage(int baseDamage)
        {
            return baseDamage + (int)Math.Round(baseDamage * _moveExecutioner.AttackMultiplier, 0);
        }
    }
}
