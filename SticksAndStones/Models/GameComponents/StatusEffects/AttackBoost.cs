using SticksAndStones.Models.GameComponents.Characters;
using System;
using System.Collections.Generic;

namespace SticksAndStones.Models.GameComponents.StatusEffects
{
    public class AttackBoost : BaseStatusEffect
    {
        private bool _applied = false;
        private double _boost;
        private int _rounds;

        public AttackBoost(CharacterBase target, double boost, int rounds = 1) : base(target)
        {
            _rounds = rounds;
            _boost = boost;
            _negative = false;
        }

        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            switch (mode)
            {
                case ProcessMode.Move:
                    if (!_applied)
                    {
                        //add attack boost to original multiplier
                        _target.AttackMultiplier += _boost;

                        //should prevent this method from being executed again during a move process mode
                        _processModes[ProcessMode.Move] = false;

                        //failsafe incase the above line does not prevent execution of this block
                        _applied = true;
                    }
                    break;
                case ProcessMode.Round:
                    if (_rounds > 0)
                    {
                        _rounds--;
                    }
                    else
                        _activeEffect = false;
                    break;
            }

            return GameError.SUCCESS;
        }

        public override void StackEffect(BaseStatusEffect effect)
        {
            AttackBoost incoming;

            if (effect.GetType() != this.GetType())
                throw new ArgumentException("only effects of same type may stack.");
            else
                incoming = (AttackBoost)effect;

            if (incoming._target != this._target)
                throw new ArgumentException("stacked effect must target same character");
            
            if (_applied)
            {
                _target.AttackMultiplier += incoming._boost;
                _rounds = _rounds > incoming._rounds ? _rounds : incoming._rounds;
                _boost += incoming._boost;
            }
        }
    }
}
