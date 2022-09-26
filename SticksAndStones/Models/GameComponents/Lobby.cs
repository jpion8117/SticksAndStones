using System.Collections.Generic;

namespace SticksAndStones.Models.GameComponents
{
    public class Lobby
    {
        private List<Player> _aTeam;
        private List<Player> _bTeam;
        private List<Player> _activeTeam;
        private List<Player> _inactiveTeam;
        private Player _currentPlayer;
        private uint _roundNumber;
        private uint _turnNumber;
        private ulong _uID;
        private List<IProcessable> _processables;

        /// <summary>
        /// gets the lobby's uniqueID.
        /// </summary>
        ulong UniqueID { get { return _uID; } }

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

        /// <summary>
        /// Indicates the total number of turns in a given round
        /// </summary>
        public uint TotalTurns
        {
            get { return (uint)(_aTeam.Count + _bTeam.Count); }
        }

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
            foreach (Player player in _inactiveTeam)
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

            }

            //incriment the turn number
            _turnNumber++;

            //check if all players have played and switch teams
            if(_activeTeam.Count == _turnNumber)
            {
                List<Player> currentlyActiveTeam = _activeTeam;
                _activeTeam = _inactiveTeam;
                _inactiveTeam = currentlyActiveTeam;
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
    }
}
