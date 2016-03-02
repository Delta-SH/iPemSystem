using System;

namespace iPem.Core.Task {
    /// <summary>
    /// Interface that should be implemented by each task
    /// </summary>
    public interface ITask {
        /// <summary>
        /// Execute task
        /// </summary>
        void Execute();

        /// <summary>
        /// Gets the name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the run period (in seconds)
        /// </summary>
        int Seconds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the thread whould be run only once (per appliction start)
        /// </summary>
        bool RunOnlyOnce { get; set; }

        /// <summary>
        /// Order
        /// </summary>
        int Order { get; set; }

        /// <summary>
        /// Enabled
        /// </summary>
        bool Enabled { get; set; }
    }
}