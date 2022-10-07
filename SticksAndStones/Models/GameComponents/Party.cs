using SticksAndStones.Models.DAL;
using Newtonsoft.Json;

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
        public Party(string jsonOptions)
        {
        }
    }
}
