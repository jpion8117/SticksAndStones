using SticksAndStones.Models.GameComponents.Characters;
using System;

namespace SticksAndStones.Models.GameComponents.StatusEffects
{
    public class Poison : BaseStatusEffect
    {
        private int _numberOfRounds;
        private int _damagePerRound;
        private bool _ignoreDefense = true;

        public Poison(CharacterBase target, int numberOfRounds, int damagePerRound, bool ignoreDefense = true) : base(target)
        {
            _negative = true;
            _numberOfRounds = numberOfRounds;
            _damagePerRound = damagePerRound;
            _ignoreDefense = ignoreDefense;
        }


        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            switch (mode)
            {
                case ProcessMode.Round:
                    if (_numberOfRounds > 0)
                    {
                        _target.TakeDamage(_damagePerRound, _ignoreDefense);
                        _numberOfRounds--;
                    }
                    else
                        _activeEffect = false; //marks effect as inactive when done so it can be removed from queue
                    break;
            }

            return GameError.SUCCESS;
        }

        public override void StackEffect(BaseStatusEffect effect)
        {
            //verify pased effect was of the same type before attempting to work with it.
            if (effect.GetType() != this.GetType())
                throw new ArgumentException($"stacked effect must be of same type as base effect! expected type of" +
                    $" {this.GetType().Name}, recieved {effect.GetType().Name}.");
            
            //cast incoming effect to poison type so it can be worked with
            var incoming = (Poison)effect;

            //adds incoming's rounds to existing rounds
            _numberOfRounds += incoming._numberOfRounds;

            //takes the highest damage per round
            _damagePerRound = _damagePerRound < incoming._damagePerRound ? incoming._damagePerRound : _damagePerRound;
        }
    }
}
