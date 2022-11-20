using SticksAndStones.Models.GameComponents.Characters;
using SticksAndStones.Models.GameComponents.StatusEffects;
using System;

namespace SticksAndStones.Models.GameComponents.Moves.Healer
{
    public class MedicalMalpractice : BaseMove
    {
        private int _rounds;
        private int _damagePerRound;
        bool _lawsuit = false;
        public MedicalMalpractice(CharacterBase executioner) : base(executioner)
        {
            var rng = new Random();
            int num = rng.Next(1, 100);

            if (num > 85)
            {
                _rounds = 3;
            }
            else if (num < 5)
            {
                _rounds = 1;
            }
            else if (num == 1)
            {
                _lawsuit = true; //poisons you for one round instead
                _rounds = 1;
            }
            else
            {
                _rounds = 2;
            }
        }

        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            CharacterBase target;

            if (_lawsuit)
                target = _moveExecutioner;
            else
                target = _targets[0];

            var poison = new Poison(target, _rounds, _damagePerRound);

            target.AddStatusEffect(poison);

            return base.ExecuteAction(mode);
        }
    }
}
