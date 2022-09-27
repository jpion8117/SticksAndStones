using System.Collections.Generic;

namespace SticksAndStones.Models.GameComponents
{
    public class UniqueIDGenerator
    {
        static private ulong _idPool = 0;
        static private List<IIdentifiable> _identifiables = new List<IIdentifiable>();
        
        /// <summary>
        /// Gets the next ID queued up in the ID Pool and queues up the next one. 
        /// in otherwords, this ID will be unique each time it is read from.
        /// </summary>
        static public ulong GetID(IIdentifiable identifiable)
        {
            ulong ID = _idPool;
            _idPool++;
            _identifiables.Add(identifiable);
            return ID;
        }

        static public IIdentifiable? GetIdentifiableByID(ulong ID)
        {
            foreach (IIdentifiable identifiable in _identifiables)
            {
                if(identifiable.UniqueID == ID)
                    return identifiable;
            }

            return null;
        }
    }
}
