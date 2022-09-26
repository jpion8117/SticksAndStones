using SticksAndStones.Models.GameComponents.StatusEffects;
using System;
using System.Collections.Generic;

namespace SticksAndStones.Models.GameComponents
{
    public class Player : IEffectable, IProcessable
    {
        protected ulong _id;
        protected string _name;
        protected int _health;
        protected int _power;
        protected float _attackMultiplyer;
        protected float _defenseMultiplyer;
        protected int _maxHealth;
        protected int _maxPower;
        protected int _decay;
        protected bool _alive;
        protected List<BaseStatusEffect> _statusEffects;

        /// <summary>
        /// Unique player ID loaded from thier user account and used internally to identify
        /// players in the battlefield.
        /// </summary>
        public ulong Id 
        { 
            get { return _id; }  
        }

        public int Health
        {
            get { return _health; }
        }
        /// <summary>
        /// Flags if player is still alive. If player is dead and value is set to true
        /// (IE another player uses a revive move) the player will be marked as alive agian
        /// and will immediatly recover 10% of their original health. All other attempts to 
        /// assign value to this will be ignored
        /// </summary>
        public bool IsAlive
        {
            get 
            {
                if( !_alive )
                {
                    _decay++; //counts the number of rounds you have been dead
                              //after 3 rounds you are no longer revivable.
                }

                return _alive; 
            }
            set
            {
                if (!_alive && value && _decay < 3)
                {
                    _health = (int)(_maxHealth * .1);
                    _alive = true;
                    _decay = 0;
                }
            }
        }
        /// <summary>
        /// gets/sets the player's current power level. WARNING: any attempt to set the powerlevel
        /// below 0 will result in an argument out of range exception! This is to prevent anyone 
        /// from using a move they don't have enough power to use. This should automatically be checked
        /// by the VerifyValidMove method, but the exception is there as a last resort to stop unintended
        /// consequences from having a negative power level.
        /// </summary>
        public int Power
        {
            get { return _power; }
        }

        public GameError addEffect(BaseStatusEffect effect)
        {
            foreach (BaseStatusEffect playerStatus in _statusEffects)
            {
                if(playerStatus.EffectGroupID == effect.EffectGroupID)
                {
                    return playerStatus.StackEffect(effect);
                }
            }

            _statusEffects.Add(effect);
            return GameError.SUCCESS;
        }

        public GameError ProcessLoop()
        {
            //player specific game processing here
            return GameError.SUCCESS;
        }

        public GameError purgeNegativeEffects()
        {
            foreach (BaseStatusEffect effect in _statusEffects)
            {
                if (effect.IsNegative)
                {
                    effect.Cure();
                }
            }

            return GameError.SUCCESS;
        }

        public GameError removeEffect(BaseStatusEffect effect)
        {
            foreach (BaseStatusEffect playerEffect in _statusEffects)
            {
                if(playerEffect.Unique_ID == effect.Unique_ID)
                {
                    playerEffect.Cure();
                    return GameError.SUCCESS;
                }
            }

            return GameError.IEFFECTABLE_EFFECT_NOT_FOUND;
        }

        public GameError updateAttack(float changeAmount)
        {
            if (_attackMultiplyer + changeAmount > -1)
            {
                return GameError.IEFFECTABLE_ARGUMENT_TOO_LOW;
            }
            else if (_attackMultiplyer + changeAmount > 1)
            {
                return GameError.IEFFECTABLE_ARGUMENT_TOO_HIGH;
            }

            _attackMultiplyer += changeAmount;
            return GameError.SUCCESS;
        }

        public GameError updateDefense(float changeAmount)
        {
            if (_defenseMultiplyer + changeAmount > -1)
            {
                return GameError.IEFFECTABLE_ARGUMENT_TOO_LOW;
            }
            else if (_defenseMultiplyer + changeAmount > 1)
            {
                return GameError.IEFFECTABLE_ARGUMENT_TOO_HIGH;
            }

            _defenseMultiplyer += changeAmount;
            return GameError.SUCCESS;
        }

        public GameError updateHealth(int changeAmount)
        {
            if (_health + changeAmount > 0)
            {
                return GameError.IEFFECTABLE_ARGUMENT_TOO_LOW;
            }
            else if (_health + changeAmount > _maxHealth)
            {
                return GameError.IEFFECTABLE_ARGUMENT_TOO_HIGH;
            }

            _health += changeAmount;
            return GameError.SUCCESS;
        }

        public GameError updatePower(int changeAmount)
        {
            if (_power + changeAmount > 0)
            {
                return GameError.IEFFECTABLE_ARGUMENT_TOO_LOW;
            }
            else if (_power +changeAmount > _maxPower)
            {
                return GameError.IEFFECTABLE_ARGUMENT_TOO_HIGH;
            }

            _power += changeAmount;
            return GameError.SUCCESS;
        }
    }
}
