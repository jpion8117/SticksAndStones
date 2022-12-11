using System.Collections.Generic;
using SticksAndStones.Models.DAL;
using System.Linq;

namespace SticksAndStones.Areas.Admin.Models
{
    public class CharacterViewModel
    {
        public CharacterViewModel(ICollection<Character> characters, IQueryable<Move> moves)
        {
            Characters = characters;
            Moves = moves;
        }

        /// <summary>
        /// Collection of all characters in database
        /// </summary>
        public ICollection<Character> Characters { get; private set; }

        private IQueryable<Move> Moves { get; set; }

        /// <summary>
        /// retrieve a collection of character's moves
        /// </summary>
        /// <param name="character">Character whoes moves you want to retrieve</param>
        public ICollection<Move> GetMoves(Character character)
        {
            var returns = Moves.Where<Move>(x => x.CharacterId == character.CharacterId).ToList();
            return returns;
        }
    }
}
