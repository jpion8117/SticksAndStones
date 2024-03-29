﻿using System.Collections.Generic;

namespace SticksAndStones.Models.GameComponents
{
    public interface IProcessable : IIdentifiable
    {
        /// <summary>
        /// Dictates what priority this entity will process at the lower the priority
        /// the sooner it will process
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// gets the completion status of a processable and 
        /// </summary>
        public bool Completed { get; }

        public Dictionary<ProcessMode, bool> ProcessModesUsed { get; }
        public GameError ExecuteAction(ProcessMode mode = ProcessMode.Move);
    }
}
