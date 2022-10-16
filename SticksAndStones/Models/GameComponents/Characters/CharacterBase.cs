using SticksAndStones.Models.GameComponents.StatusEffects;
using System;
using System.Collections.Generic;

namespace SticksAndStones.Models.GameComponents.Characters
{
    public class CharacterBase : IProcessable
    {
        protected ulong _id;
        protected int _health;
        protected int _power;
        protected float _attackMultiplyer;
        protected float _defenseMultiplyer;
        protected int _maxHealth;
        protected int _maxPower;
        protected int _decay;
        protected bool _alive;
        protected int _processPriority;
        protected Lobby _lobby;

        public CharacterBase()
        {
            _id = UniqueIDGenerator.GetID(this);
        }

        /// <summary>
        /// Unique player ID loaded from thier user account and used internally to identify
        /// players in the battlefield.
        /// </summary>
        public ulong UniqueID
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
                if (!_alive)
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

        public bool Completed { get { return false; } } //the player will remain in the process queue as
                                                        //long as the game is going. This is to ensure
                                                        //players who are revived will remain in the
                                                        //queue
        public int Priority { get { return 100; } } //effects and other things that may effect

        public string Type { get { return "Player"; } }

        public object IdentifiableObject { get { return this; } }

        //player health need to process first

        //public GameError addEffect(BaseStatusEffect effect)
        //{
        //    foreach (BaseStatusEffect playerStatus in _statusEffects)
        //    {
        //        if (playerStatus.EffectGroupID == effect.EffectGroupID)
        //        {
        //            return playerStatus.StackEffect(effect);
        //        }
        //    }

        //    _statusEffects.Add(effect);
        //    return GameError.SUCCESS;
        //}

        public GameError ExecuteAction()
        {
            //check if player is still alive
            if (_health <= 0)
                _alive = false;

            ////remove any expired effects
            //foreach (BaseStatusEffect playerStatus in _statusEffects)
            //{
            //    if (playerStatus.Completed)
            //    {
            //        _statusEffects.Remove(playerStatus);
            //    }
            //}

            return GameError.SUCCESS;
        }

        //public GameError purgeNegativeEffects()
        //{
        //    foreach (BaseStatusEffect effect in _statusEffects)
        //    {
        //        if (effect.IsNegative)
        //        {
        //            effect.Cure();
        //        }
        //    }

        //    return GameError.SUCCESS;
        //}

        //public GameError removeEffect(BaseStatusEffect effect)
        //{
        //    foreach (BaseStatusEffect playerEffect in _statusEffects)
        //    {
        //        if (playerEffect.UniqueID == effect.UniqueID)
        //        {
        //            playerEffect.Cure();
        //            return GameError.SUCCESS;
        //        }
        //    }

        //    return GameError.IEFFECTABLE_EFFECT_NOT_FOUND;
        //}

        /// <summary>
        /// Attack multiplier used to determine how much additional damage is added (or removed) from the 
        /// base attack damage
        /// </summary>
        public float AttackMultiplier
        {
            get { return _attackMultiplyer; }
            set 
            { 
                if(value < -1 || value > 1)
                    throw new ArgumentOutOfRangeException("Attack multiplier must be between -1 and 1 (-100% and 100%)");

                _attackMultiplyer = value; 
            }
        }
        /// <summary>
        /// Defense multiplier used to determine how much additional damage is blocked from the 
        /// base attack damage negative values have a chance of adding additional damage to an attack
        /// </summary>
        public float DefenseMultiplier
        {
            get { return _defenseMultiplyer; }
            set
            {
                if (value < -1 || value > 1)
                    throw new ArgumentOutOfRangeException("Defense multiplier must be between -1 and 1 (-100% and 100%)");

                _defenseMultiplyer = value;
            }
        }

        /// <summary>
        /// Used for both attack (negative health effects) and healing (positive health effects) moves. When supplied a
        /// negative value (an attack) method will factor in character's defense level to calculate the final damage 
        /// value
        /// </summary>
        /// <param name="changeAmount">Amount of healt change requested</param>
        /// <returns>Game error or success code</returns>
        public GameError updateHealth(int changeAmount)
        {
            if (changeAmount < 0) //indicates an attack action
                changeAmount = changeAmount + (int)((float)changeAmount * _defenseMultiplyer);

            //health cannot drop below 0 only to 0
            if (_health + changeAmount > 0)
            {
                _health = 0;
                return GameError.SUCCESS;
            }
            else if (_health + changeAmount > _maxHealth)
            {
                return GameError.IEFFECTABLE_ARGUMENT_TOO_HIGH;
            }

            //apply the health change
            _health += changeAmount;
            return GameError.SUCCESS;
        }

        /// <summary>
        /// used to update a character's power level primarily when removing power from a character using a 
        /// special move, but can also add power to a player through special moves.
        /// </summary>
        /// <param name="changeAmount">Amount of power change</param>
        /// <returns>GameError error or success code</returns>
        public GameError updatePower(int changeAmount)
        {
            if (_power + changeAmount > 0)
            {
                return GameError.IEFFECTABLE_ARGUMENT_TOO_LOW;
            }
            else if (_power + changeAmount > _maxPower)
            {
                return GameError.IEFFECTABLE_ARGUMENT_TOO_HIGH;
            }

            _power += changeAmount;
            return GameError.SUCCESS;
        }
    }
}
