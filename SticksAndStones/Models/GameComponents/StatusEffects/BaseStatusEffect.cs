namespace SticksAndStones.Models.GameComponents.StatusEffects
{
    abstract public class BaseStatusEffect : IProcessable
    {
        private static int _nextID = 99999;
        private static int _nextGroupID = -1;
        private int _uID = 0;
        protected IEffectable _target;
        protected bool _activeEffect;
        protected bool _negative;

        /// <summary>
        /// Available internally to all child classes for assigning unique group IDs
        /// _nextGroupID will increment each time it is accessed through this to ensure no
        /// 2 IDs are ever the same.
        /// </summary>
        protected int NextGroupID
        {
            get
            {
                _nextGroupID++;
                return _nextGroupID;
            }
        }

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
        /// Marks if the effect is currently active or if it is deactive and needs to be 
        /// cleared.
        /// </summary>
        public bool IsActiveEffect
        {
            get { return _activeEffect; }
        }

        /// <summary>
        /// on the off chance there was an error and a uID did not get assigned, 
        /// this will assign one before an attempt to access it is made.
        /// </summary>
        public int Unique_ID
        {
            get
            {
                if ( _uID == 0 )
                {
                    _nextID++;
                    _uID = _nextID;
                }
                return _uID;
            }
        }

        /// <summary>
        /// this constructor has 1 job, make sure every status effect instanciated
        /// has a unique ID assigned at runtime to keep track of them
        /// </summary>
        protected BaseStatusEffect()
        {
            _nextID++;
            _uID = _nextID;
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

        public abstract GameError ProcessLoop();
    }
}
