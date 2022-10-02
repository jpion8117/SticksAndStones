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

        /// <summary>
        /// Search for an 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        static public IIdentifiable? GetIdentifiableByID(ulong uID)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            foreach (IIdentifiable identifiable in _identifiables)
            {
                if(identifiable.UniqueID == uID)
                    return identifiable;
            }

            return null;
        }
    }
}
