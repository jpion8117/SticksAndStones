namespace SticksAndStones.Models.GameComponents
{
    public class UniqueIDGenerator
    {
        static private ulong _idPool = 0;
        
        /// <summary>
        /// Gets the next ID queued up in the ID Pool and queues up the next one. 
        /// in otherwords, this ID will be unique each time it is read from.
        /// </summary>
        static public ulong NextID
        {
            get
            {
                ulong uID = _idPool;
                _idPool++;
                return uID;
            }
        }
    }
}
