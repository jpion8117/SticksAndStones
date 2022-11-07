namespace SticksAndStones.Models.GameComponents
{
    public enum GameError
    {
        /// <summary>
        /// No errors occured in method
        /// </summary>
        SUCCESS,

        /// <summary>
        /// A general failure occured, this flag should almost never be 
        /// used as it's ambiguity makes debugging more difficult
        /// </summary>
        GENERAL_FAILURE,

        /// <summary>
        /// used in the move classes to denote a duplicate target when
        /// attempting to add a new target to a move. Can be used by 
        /// front end to inform user of invalid selection.
        /// </summary>
        MOVE_TARGET_DUPLICATE,

        /// <summary>
        /// used in the move classes when attempting to locate a targeted
        /// player who was not found. Can be used by front end to inform
        /// user of invalid selection.
        /// </summary>
        MOVE_TARGET_NOT_FOUND,

        /// <summary>
        /// used by move classes when attempting to add a target to the targets list
        /// if the list is already full. Can be fed back to user to inform them
        /// of invalid selection
        /// </summary>
        MOVE_MAX_TARGETS_EXCEEDED,

        /// <summary>
        /// used by move classes to inform player that they don't have enough power to complete the
        /// requested move.
        /// </summary>
        MOVE_NOT_ENOUGH_POWER,

        /// <summary>
        /// used by move classes to indicate an opperation occured that shouldn't have proceeded without
        /// at least one target defined.
        /// </summary>
        MOVE_NO_TARGETS_DEFINED,

        /// <summary>
        /// used by IEffectable objects for changes that are too high above acceptable range
        /// </summary>
        GENERAL_ARGUMENT_TOO_HIGH,

        /// <summary>
        /// used by IEffectable objects for changes that are too low below acceptable range
        /// </summary>
        GENERAL_ARGUMENT_TOO_LOW,

        /// <summary>
        /// Used by IEffectable to indicate an effect that was being searched for was not found
        /// </summary>
        IEFFECTABLE_EFFECT_NOT_FOUND,

        /// <summary>
        /// Used to indicate an ID was not found when searching a list by UniqueID.
        /// </summary>
        GENERAL_UNIQUE_ID_NOT_FOUND,

        /// <summary>
        /// Used when adding something to a list to indicate it already exists within that list
        /// </summary>
        GENERAL_ALREADY_IN_QUEUE,

        /// <summary>
        /// Sent by the lobby turn processor when all players on team B are dead
        /// </summary>
        LOBBY_TEAM_A_WINS,

        /// <summary>
        /// Sent by the lobby turn processor when all players on team A are dead
        /// </summary>
        LOBBY_TEAM_B_WINS,
        /// <summary>
        /// Sent by moves that are invalid
        /// </summary>
        MOVE_INVALID,
        /// <summary>
        /// IProcessable failed to execute during execution method
        /// </summary>
        IPROCESSABLE_FAILED_EXE
    }
}
