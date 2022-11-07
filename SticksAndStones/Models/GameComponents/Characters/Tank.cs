using SticksAndStones.Models.GameComponents.Moves.Tank;

namespace SticksAndStones.Models.GameComponents.Characters
{
    public class Tank : CharacterBase
    {
        public Tank()
        {
            _maxHealth = 100;
            _health = _maxHealth;
            _defenseMultiplyer = 0.5f;
            _attackMultiplyer = -0.4f;

            //populate moves list
            _moveList.Add(new BulletSponge(this));
            _moveList.Add(new BloodDonor(this));
            _moveList.Add(new BodySlam(this));
            _moveList.Add(new ManOfSteelSpecial(this));
        }
    }
}
