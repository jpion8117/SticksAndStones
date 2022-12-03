using SticksAndStones.Models.GameComponents.Characters;
using SticksAndStones.Models.DAL;
using System;
using System.Collections.Generic;

namespace SticksAndStones.Models.GameComponents
{
    public class Lobby
    {
        private Party _aParty;
        private Party _bParty;
        private Party _activeParty;
        private Party _inactiveParty;
        private LobbyRegistration _registration;
        private uint _roundNumber;
        private uint _turnNumber;
        private uint _partySize;
        private List<IProcessable> _processables;
        private bool _processablesSorted = false;

        private static List<Lobby> _activeLobbies = new List<Lobby>();

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
        /// Used internally to retrieve a list of all processable entities sorted by priority 
        /// from highest (lowest values) to lowest (highest values) if needed.
        /// </summary>
        private List<IProcessable> Processables
        {
            get 
            {
                if(!_processablesSorted)
                {
                    _processables.Sort((a, b) => a.Priority.CompareTo(b.Priority));
                    _processablesSorted = true;
                }

                return _processables;
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
            foreach (IProcessable processableEntity in Processables)
            {

                GameError error = GameError.SUCCESS;

                //purge any processableEntities that have marked all tasks as complete, skip
                //processing on entities that have ProcessMode.Move set to false, and process
                //any that remain
                if (processableEntity.Completed)
                    _processables.Remove(processableEntity);
                else if (!processableEntity.ProcessModesUsed[ProcessMode.Move])
                    continue;
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

                //process turn level actions
                foreach(IProcessable processable in Processables)
                {
                    //check if there is a key for turn level events and get its value if there is otherwise
                    //continue loop. This should skip unneeded method calls by only calling ExecuteAction 
                    //on processables that have code to run.
                    bool containsTurnEvents;
                    if (!processable.ProcessModesUsed.TryGetValue(ProcessMode.Turn, out containsTurnEvents))
                        continue;

                    //run any actions that happen on the turn level
                    if(containsTurnEvents)
                        processable.ExecuteAction(ProcessMode.Turn);
                }

                //if party a is now the active party, incriment round number.
                if (_aParty.User.Id == _activeParty.User.Id)
                {

                    //process round level actions
                    foreach (IProcessable processable in Processables)
                    {
                        //check if there is a key for round level events and get its value if there is otherwise
                        //continue loop. This should skip unneeded method calls by only calling ExecuteAction 
                        //on processables that have code to run.
                        bool containsRoundEvents;
                        if (!processable.ProcessModesUsed.TryGetValue(ProcessMode.Round, out containsRoundEvents))
                            continue;

                        //run any actions that happen on the turn level
                        if (containsRoundEvents)
                            processable.ExecuteAction(ProcessMode.Round);
                    }

                    _roundNumber++;
                }
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
            _processablesSorted = false;
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
        static public Lobby GetLobbyByID(int id)
        {
            foreach (Lobby lobby in _activeLobbies)
            {
                if(lobby._registration.LobbyId == id)
                {
                    return lobby;
                }
            }

            return null;
        }
    }
}
