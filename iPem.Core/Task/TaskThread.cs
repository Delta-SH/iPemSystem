using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace iPem.Core.Task {
    /// <summary>
    /// Represents task thread
    /// </summary>
    public class TaskThread : IDisposable {
        private Timer _timer;
        private bool _disposed;
        private readonly Dictionary<string, ITask> _tasks;

        internal TaskThread() {
            this._tasks = new Dictionary<string, ITask>();
            this.Seconds = 60;
        }

        private void Run() {
            if(Seconds <= 0)
                return;

            this.StartedUtc = DateTime.UtcNow;
            this.IsRunning = true;

            var tasks = this._tasks.Values.AsQueryable().OrderBy(t => t.Order);
            foreach(var task in tasks) {
                if(!task.Enabled) continue;

                task.Execute();
            }
            this.IsRunning = false;
        }

        private void TimerHandler(object state) {
            this._timer.Change(-1, -1);
            this.Run();
            if(this.RunOnlyOnce) {
                this.Dispose();
            } else {
                this._timer.Change(this.Interval, this.Interval);
            }
        }

        /// <summary>
        /// Disposes the instance
        /// </summary>
        public void Dispose() {
            if((this._timer != null) && !this._disposed) {
                lock(this) {
                    this._timer.Dispose();
                    this._timer = null;
                    this._disposed = true;
                }
            }
        }

        /// <summary>
        /// Starts the task thread
        /// </summary>
        public void Start() {
            if(this._timer == null) {
                this._timer = new Timer(new TimerCallback(this.TimerHandler), null, this.Interval, this.Interval);
            }
        }

        /// <summary>
        /// Adds a task to the thread
        /// </summary>
        /// <param name="task">The task to be added</param>
        public void AddTask(ITask task) {
            if(!this._tasks.ContainsKey(task.Name)) {
                this._tasks.Add(task.Name, task);
            }
        }


        /// <summary>
        /// Gets or sets the interval in seconds at which to run the tasks
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// Get or sets a datetime when thread has been started
        /// </summary>
        public DateTime StartedUtc { get; private set; }

        /// <summary>
        /// Get or sets a value indicating whether thread is running
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Get a list of tasks
        /// </summary>
        public IList<ITask> Tasks {
            get {
                return new ReadOnlyCollection<ITask>(this._tasks.Values.ToList());
            }
        }

        /// <summary>
        /// Gets the interval at which to run the tasks
        /// </summary>
        public int Interval {
            get {
                return this.Seconds * 1000;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the thread whould be run only once (per appliction start)
        /// </summary>
        public bool RunOnlyOnce { get; set; }
    }
}