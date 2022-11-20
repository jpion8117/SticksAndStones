using SticksAndStones.Models.GameComponents.Characters;
using System.Collections.Generic;

namespace SticksAndStones.Models.GameComponents.StatusEffects
{
    abstract public class BaseStatusEffect : IProcessable
    {
        private ulong _uID = 0;
        protected CharacterBase _target;
        protected bool _activeEffect;
        protected bool _negative;
        protected Dictionary<ProcessMode, bool> _processModes = new Dictionary<ProcessMode, bool>();
        
        /// <summary>
        /// Marks whether the effect of the status effect is a net negative for the entity it is 
        /// applied to.
        /// </summary>
        public bool IsNegative
        {
            get { return _negative; }
        }

        public ulong UniqueID
        {
            get { return _uID; }
        }

        public bool Completed { get { return !_activeEffect; } } //true when effect marked inactive

        public int Priority { get { return 2; } }

        /// <summary>
        /// this constructor has 1 job, make sure every status effect instanciated
        /// has a unique ID assigned at runtime to keep track of them
        /// </summary>
        protected BaseStatusEffect(CharacterBase target)
        {
            _target = target;
            _uID = UniqueIDGenerator.GetID(this);
            _processModes[ProcessMode.Move] = false;
            _processModes[ProcessMode.Turn] = false;
            _processModes[ProcessMode.Round] = true;
        }

        /// <summary>
        /// Marks status effect as inactive and can be overridden for additional functionality
        /// </summary>
        public virtual GameError Cure()
        {
            _activeEffect = false;
            return GameError.SUCCESS;
        }

        /// <summary>
        /// Defines how status effects behave when two effects are applied to the same 
        /// character based on the specific effect.
        /// </summary>
        /// <param name="effect">Incomming status effect</param>
        public abstract void StackEffect(BaseStatusEffect effect);

        public abstract GameError ExecuteAction(ProcessMode mode = ProcessMode.Move);

        public Dictionary<ProcessMode, bool> ProcessModesUsed => _processModes;
    }
}
