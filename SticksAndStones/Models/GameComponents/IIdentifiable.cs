namespace SticksAndStones.Models.GameComponents
{
    public interface IIdentifiable
    {
        /// <summary>
        /// Identifiable object type
        /// </summary>
        public string Type { get; }
        public ulong UniqueID { get; }

        public object IdentifiableObject { get; }
    }
}
