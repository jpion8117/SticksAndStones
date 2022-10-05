using SticksAndStones.Models.DAL;
using System.Collections.Generic;

namespace SticksAndStones.Models.GameComponents
{
    public class Party
    {
        private User _user;
        private List<CharacterBase> _characters;


        public User User { get { return _user; } }
        public List<CharacterBase> Characters { get { return _characters; } }
        public Party(User user, List<CharacterBase> characters)
        {
            _user = user;
            _characters = characters;
        }
    }
}
