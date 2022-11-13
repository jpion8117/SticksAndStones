namespace SticksAndStones.Models.GameComponents
{
    public enum ProcessMode
    {
        /// <summary>
        /// This is the default process mode run after each move a player makes, everytime an
        /// IProcessable is executed move level events will run first.
        /// </summary>
        Move,

        /// <summary>
        /// This process mode should run events that need to be run after every turn and 
        /// run only once move level events have been processed
        /// </summary>
        Turn,

        /// <summary>
        /// This process mode should run after all other process events have run and only be
        /// used once both parties have played all of thier characters.
        /// </summary>
        Round
    }
}
