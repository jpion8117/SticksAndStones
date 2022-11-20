using SticksAndStones.Models.GameComponents.Characters;
using System;
using System.Linq;

namespace SticksAndStones.Models.GameComponents.StatusEffects
{
    public class HealthOverTime : BaseStatusEffect
    {
        private int _numberOfRounds;
        private int _healthAddedPerRound;

        public HealthOverTime(CharacterBase target, int numberOfRounds, int healthPerRound) : base(target)
        {
            _numberOfRounds = numberOfRounds;
            _healthAddedPerRound = healthPerRound;
        }

        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            switch (mode)
            {
                case ProcessMode.Round:
                    if (_numberOfRounds > 0)
                    {
                        _target.UpdateHealth(_healthAddedPerRound);
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
            var incoming = (HealthOverTime)effect;

            //adds incoming's rounds to existing rounds
            _numberOfRounds += incoming._numberOfRounds;

            //create an array to average both current and incoming healthAddedPerRound values
            int[] healthAdd = { _healthAddedPerRound, incoming._healthAddedPerRound };
            
            //takes the highest damage per round
            _healthAddedPerRound = (int)healthAdd.Average();
        }
    }
}
