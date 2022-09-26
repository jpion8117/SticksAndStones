namespace SticksAndStones.Models.GameComponents.StatusEffects
{
    abstract public class BaseStatusEffect : IProcessable
    {
        private ulong _uID = 0;
        protected IEffectable _target;
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

        /// <summary>
        /// on the off chance there was an error and a uID did not get assigned, 
        /// this will assign one before an attempt to access it is made.
        /// </summary>
        public ulong UniqueID
        {
            get { return _uID; }
        }

        public bool Completed { get { return !_activeEffect; } } //true when effect marked inactive

        /// <summary>
        /// this constructor has 1 job, make sure every status effect instanciated
        /// has a unique ID assigned at runtime to keep track of them
        /// </summary>
        protected BaseStatusEffect()
        {
            _uID = UniqueIDGenerator.NextID;
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

        public abstract GameError ExecuteAction();
    }
}
