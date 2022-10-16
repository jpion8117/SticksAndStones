using SticksAndStones.Models.GameComponents.Characters;
using System;
using System.Collections.Generic;

namespace SticksAndStones.Models.GameComponents
{
    public class Lobby : IIdentifiable
    {
        private Party _aParty;
        private Party _bParty;
        private Party _activeParty;
        private Party _inactiveParty;
        private uint _roundNumber;
        private uint _turnNumber;
        private ulong _uID;
        private uint _partySize;
        private List<IProcessable> _processables;

        private static List<Lobby> _activeLobbies = new List<Lobby>();


        /// <summary>
        /// gets the lobby's uniqueID.
        /// </summary>
        public ulong UniqueID { get { return _uID; } }

        /// <summary>
        /// Indicates which round the game is currently on.
        /// </summary>
        public uint RoundNumber
        {
            get { return _roundNumber; }
        }

        /// <summary>
        /// Indicates which turn the round is currently on.
        /// </summary>
        public uint TurnNumber
        {
            get { return _turnNumber; }
        }

        public uint PartySize
        {
            get { return _partySize; }
            set 
            {
                //make sure party size is valid
                if(value < 2 || value > 4)
                {
                    throw new ArgumentException("Value of 'PartySize' must be between 2 and 4");
                }

                _partySize = value; 
            }
        }

        /// <summary>
        /// Indicates the total number of turns in a given round
        /// </summary>
        public uint TotalTurns
        {
            get { return (uint)(_aParty.Members.Length + _bParty.Members.Length); }
        }


        public string PlayerAName
        {
            get
            {
                return _aParty.User.UserName;
            }
        }
        public string PlayerBName
        {
            get
            {
                return _bParty.User.UserName;
            }
        }
        public string Type { get { return "Lobby"; } }

        public object IdentifiableObject { get { return this; } }

        /// <summary>
        /// Processes all data between turns including: Applying any status effects, 
        /// incrementing turn counters, signaling who's turn it is so their input can
        /// be unlocked, etc
        /// </summary>
        /// <returns>GameError.SUCCESS upon successfull completion or a GameError
        /// code that indicates what happened</returns>
        public List<(GameError, ulong)> ProcessTurn()
        {
            //stores a list of errors generated while processing processables and their ID's
            List<(GameError, ulong)> results = new List<(GameError, ulong)> ();

            //process each processableEntity
            foreach (IProcessable processableEntity in _processables)
            {
                GameError error = GameError.SUCCESS;

                //will purge processable entity if it's actions are complete or 
                //execute it if not.
                if (processableEntity.Completed)
                    _processables.Remove(processableEntity);
                else
                    error = processableEntity.ExecuteAction();

                //checks to see if there was an error processing the entity and adds the error
                //and the offending UniqueID to the results list that is returned by the method
                if (error != GameError.SUCCESS)
                    results.Add((error, processableEntity.UniqueID));
            }

            //check if all players on the inactive team are dead
            bool activeTeamWins = true;
            foreach (CharacterBase player in _inactiveParty.Members)
            {
                //if at least one team member is still alive, the game continues
                if (player.IsAlive)
                {
                    activeTeamWins = false;
                    break;
                }
            }
            if (activeTeamWins)
            {
                //process end of game
            }

            //incriment the turn number
            _turnNumber++;

            //check if all players have played then switch teams and incriment the round counter
            if(_activeParty.Members.Length == _turnNumber)
            {
                //temp storage of active team
                Party currentlyActiveTeam = _activeParty;
                
                //assign inactive party to active
                _activeParty = _inactiveParty;

                //assign former active party to inactive party
                _inactiveParty = currentlyActiveTeam;

                //reset turn number
                _turnNumber = 0;

                //if party a is now the active party, incriment round number.
                if (_aParty.User.UserID == _activeParty.User.UserID)
                    _roundNumber++;
            }

            return results;
        }

        /// <summary>
        /// Adds a processable entity to the processable queue after checking for duplicates
        /// </summary>
        /// <param name="processable">Processable entity to add to the queue</param>
        /// <returns></returns>
        public GameError AddToProcessQueue(IProcessable processable)
        {
            foreach(IProcessable processableEntity in _processables)
            {
                if (processableEntity.UniqueID == processable.UniqueID)
                    return GameError.GENERAL_ALREADY_IN_QUEUE;
            }

            _processables.Add(processable);
            return GameError.SUCCESS;
        }

        public GameError RemoveFromProcessQueue(IProcessable processable)
        {
            foreach (IProcessable processableEntity in _processables)
            {
                if (processableEntity.UniqueID == processable.UniqueID)
                {
                    _processables.Remove(processableEntity);
                    return GameError.SUCCESS;
                }
            }

            return GameError.GENERAL_UNIQUE_ID_NOT_FOUND;
        }
        static public Lobby GetLobbyByID(ulong id)
        {
            foreach (Lobby lobby in _activeLobbies)
            {
                if(lobby.UniqueID == id)
                {
                    return lobby;
                }
            }

            return null;
        }
    }
}
