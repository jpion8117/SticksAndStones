using SticksAndStones.Models.DAL;
using Newtonsoft.Json;
using System.Collections.Generic;
using SticksAndStones.Models.GameComponents.Characters;

namespace SticksAndStones.Models.GameComponents
{
    public class Party : IIdentifiable
    {
        private User _user;
        private CharacterBase[] _members;
        private ulong _uniqueID;

        public User User { get { return _user; } }
        public CharacterBase[] Members { get { return _members; } }

        public string Type => "Party";

        public ulong UniqueID => _uniqueID;

        public object IdentifiableObject => this;

        public Party(User user, CharacterBase[] members)
        {
            _user = user;
            _members = members;
        }
        public Party(User user, string[] members)
        {
            _uniqueID = UniqueIDGenerator.GetID(this);
            _user = user;

            List<CharacterBase> membersLocal = new List<CharacterBase>();
            foreach (string member in members)
            {
                switch (member)
                {
                    default:
                        membersLocal.Add(new CharacterBase());
                        break;
                }
            }

            _members = membersLocal.ToArray();
        }
    }
}
