using SticksAndStones.Models.GameComponents.Moves;
using SticksAndStones.Models.GameComponents.Moves.Shared;
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
        protected double _attackMultiplyer;
        protected double _defenseMultiplyer;
        protected int _maxHealth;
        protected int _maxPower;
        protected int _decay;
        protected bool _alive;
        protected int _redirectCount;
        protected CharacterBase _redirectAttackTarget;
        protected List<BaseMove> _moveList;
        protected Dictionary<ProcessMode, bool> _processModes = new Dictionary<ProcessMode, bool>();
        private ulong _partyID;

        public CharacterBase()
        {
            _id = UniqueIDGenerator.GetID(this);
            _power = 10;
            _maxPower = 20;
            _alive = true;
            _decay = 0;
            _redirectCount = 0;
            _redirectAttackTarget = null;
            _partyID = 0;
            _moveList = new List<BaseMove>();
            _moveList.Add(new StandardAttack(this));
            _moveList.Add(new StandardBlock(this));
            _processModes.Add(ProcessMode.Move, true);
            _processModes.Add(ProcessMode.Round, true);
        }

        /// <summary>
        /// Unique player ID loaded from thier user account and used internally to identify
        /// players in the battlefield.
        /// </summary>
        public ulong UniqueID
        {
            get { return _id; }
        }

        public ulong PartyID 
        { 
            get { return _partyID; }
            set 
            {
                //values less than 1000 used for testing purposes
                if(value < 1000)
                {
                    _partyID = value;
                }
                else
                { 
                    _partyID = UniqueIDGenerator.GetIdentifiableByID(value).Type == "Party" ? value : 0;
                }
            } 
        }

        public int Health
        {
            get { return _health; }
        }
        public bool CanRevive
        {
            get { return _decay < 3; }
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

                _alive = _health > 0;
                return _alive;
            }
            set
            {
                if (!_alive && value && CanRevive)
                {
                    _health = (int)(_maxHealth * .2);
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

        public CharacterBase RedirectAttackTarget
        {
            get 
            {
                if (_redirectCount == 0 || _redirectAttackTarget == null)
                {
                    _redirectAttackTarget = null;
                    return _redirectAttackTarget;
                }

                return _redirectAttackTarget; 
            }
        }

        /// <summary>
        /// Set both the number of attacks that will be redirected to a given target and how many 
        /// times damage will be redirected to that target default is one
        /// </summary>
        /// <param name="redirectTarget">Character who will absorb attacks</param>
        /// <param name="numberOfRedirects">how many times the attacks will be redirected</param>
        public void SetRedirect(CharacterBase redirectTarget, int numberOfRedirects = 1)
        {
            _redirectAttackTarget = redirectTarget;
            _redirectCount = numberOfRedirects;
        }

        public string Type { get { return "Player"; } }

        public object IdentifiableObject { get { return this; } }

        public GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            switch (mode)
            {
                case ProcessMode.Move:
                    //check if player is still alive
                    if (_health <= 0)
                        _alive = false;
                    break;
                case ProcessMode.Round:

                    //check for redirects and reduce redirect count if needed
                    if (_redirectCount != 0)
                        _redirectCount--;
                    break;
            }

            return GameError.SUCCESS;
        }

        /// <summary>
        /// Attack multiplier used to determine how much additional damage is added (or removed) from the 
        /// base attack damage
        /// </summary>
        public double AttackMultiplier
        {
            get { return _attackMultiplyer; }
            set 
            { 
                if(value < -1 || value > 5)
                    throw new ArgumentOutOfRangeException("Attack multiplier must be between -1 and 5 (-100% and 500%)");

                _attackMultiplyer = Math.Round(1 - value, 2); 
            }
        }
        /// <summary>
        /// Defense multiplier used to determine how much additional damage is blocked from the 
        /// base attack damage negative values have a chance of adding additional damage to an attack
        /// </summary>
        public double DefenseMultiplier
        {
            get { return _defenseMultiplyer; }
            set
            {
                if (value < -5 || value > 1)
                    throw new ArgumentOutOfRangeException("Defense multiplier must be between -5 and 1 (-500% and 100%)");

                _defenseMultiplyer = Math.Round(1 - value, 2);
            }
        }

        public Dictionary<ProcessMode, bool> ProcessModesUsed => _processModes;

        /// <summary>
        /// Used for both attack (negative health effects) and healing (positive health effects) moves. When supplied a
        /// negative value (an attack) method will factor in character's defense level to calculate the final damage 
        /// value
        /// </summary>
        /// <param name="changeAmount">Amount of health change requested</param>
        /// <returns>Game error or success code</returns>
        public GameError updateHealth(int changeAmount)
        {
            if (changeAmount < 0) //indicates an attack action
                return GameError.GENERAL_ARGUMENT_TOO_LOW;

            else if (_health + changeAmount > _maxHealth)
            {
                _health = _maxHealth;
                return GameError.SUCCESS;
            }

            //apply the health change
            _health += changeAmount;
            return GameError.SUCCESS;
        }

        /// <summary>
        /// Apply damage to a character
        /// </summary>
        /// <param name="damage">Amount of damage character will take</param>
        /// <param name="ignoreDefense">If set to true, damage dealt will ignore defense multiplier. Default 
        /// value is false</param>
        /// <returns></returns>
        public GameError TakeDamage(int damage, bool ignoreDefense = false)
        {
            if (damage < 0)
            {
                return GameError.GENERAL_ARGUMENT_TOO_LOW;
            }

            //check for redirects
            if (RedirectAttackTarget != null)
            {
                _redirectAttackTarget.TakeDamage(damage, ignoreDefense);
                return GameError.SUCCESS;
            }

            //health may go no lower than 0
            if (_health - (ignoreDefense ? damage : (int)((float)damage * _defenseMultiplyer)) < 0)
            {
                _health = 0;
            }
            else
            {
                _health = ignoreDefense ? _health - damage : _health - (int)((float)damage * _defenseMultiplyer);
            }

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
            if (_power + changeAmount < 0)
            {
                return GameError.GENERAL_ARGUMENT_TOO_LOW;
            }
            else if (_power + changeAmount > _maxPower)
            {
                _power = _maxPower;
            }

            _power += changeAmount;
            return GameError.SUCCESS;
        }
    }
}
