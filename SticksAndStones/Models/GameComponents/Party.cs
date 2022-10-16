using SticksAndStones.Models.DAL;
using Newtonsoft.Json;
using System.Collections.Generic;
using SticksAndStones.Models.GameComponents.Characters;

namespace SticksAndStones.Models.GameComponents
{
    public class Party
    {
        private User _user;
        private CharacterBase[] _members;


        public User User { get { return _user; } }
        public CharacterBase[] Members { get { return _members; } }
        public Party(User user, CharacterBase[] members)
        {
            _user = user;
            _members = members;
        }
        public Party(User user, string[] members)
        {
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
