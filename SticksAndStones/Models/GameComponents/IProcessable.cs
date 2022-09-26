namespace SticksAndStones.Models.GameComponents
{
    public interface IProcessable
    {
        /// <summary>
        /// Dictates what priority this entity will process at the lower the priority
        /// the sooner it will process
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// gets the entity's processable ID
        /// </summary>
        public ulong UniqueID { get; }

        /// <summary>
        /// gets the completion status of a processable and 
        /// </summary>
        public bool Completed { get; }
        public GameError ExecuteAction();
    }
}
