using SticksAndStones.Models.GameComponents.Characters;

namespace SticksAndStones.Models.GameComponents.StatusEffects
{
    abstract public class BaseStatusEffect : IProcessable
    {
        private ulong _uID = 0;
        protected CharacterBase _target;
        protected bool _activeEffect;
        protected bool _negative;

        /// <summary>
        /// Provides access to each status effect's group ID (shared among all instances of 
        /// that given effect) which is auto-assigned at runtime
        /// </summary>
        abstract public int EffectGroupID { get; }
        
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

        public object IdentifiableObject { get { return this; } }

        public bool Completed { get { return !_activeEffect; } } //true when effect marked inactive

        public int Priority { get { return 2; } }

        /// <summary>
        /// this constructor has 1 job, make sure every status effect instanciated
        /// has a unique ID assigned at runtime to keep track of them
        /// </summary>
        protected BaseStatusEffect()
        {
            _uID = UniqueIDGenerator.GetID(this);
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
        /// used to combine the effects of 2 different statuses into one if applicable.
        /// </summary>
        /// <param name="effect">Incomming status effect</param>
        public abstract GameError StackEffect(BaseStatusEffect effect);

        public abstract GameError ExecuteAction(ProcessMode mode = ProcessMode.Move);

        public abstract string Type { get; }
    }
}
