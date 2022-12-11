using SticksAndStones.Models.DAL;
using System.Collections.Generic;

namespace SticksAndStones.Models.ViewModels
{
    public class FighterInfoViewModel
    {
        public List<Character> CharacterList { get; set; }
        public List<Move> Moves { get; set; }
        public List<Effect> Effects { get; set; }
        public List<Move> GetCharacterMoves(Character character)
        {
            return character.Moves as List<Move>;
        }
    }
}
