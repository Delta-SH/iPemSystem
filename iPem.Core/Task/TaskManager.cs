using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace iPem.Core.Task {
    /// <summary>
    /// Represents task manager
    /// </summary>
    public class TaskManager {
        private static readonly TaskManager _taskManager = new TaskManager();
        private readonly List<TaskThread> _taskThreads = new List<TaskThread>();

        private TaskManager() { }

        /// <summary>
        /// Initializes the task manager with the specified tasks.
        /// </summary>
        public void Initialize(List<ITask> scheduleTasks) {
            this._taskThreads.Clear();

            var onceTasks = scheduleTasks
                .Where(x => x.RunOnlyOnce)
                .ToList();

            if(onceTasks.Count > 0) {
                foreach(var scheduleTaskGrouped in onceTasks.GroupBy(x => x.Seconds)) {
                    var taskThread = new TaskThread {
                        RunOnlyOnce = true,
                        Seconds = scheduleTaskGrouped.Key
                    };
                    foreach(var scheduleTask in scheduleTaskGrouped) {
                        taskThread.AddTask(scheduleTask);
                    }
                    this._taskThreads.Add(taskThread);
                }
            }

            var moreTasks = scheduleTasks
                .Where(x => !x.RunOnlyOnce)
                .ToList();

            if(moreTasks.Count > 0) {
                foreach(var scheduleTaskGrouped in moreTasks.GroupBy(x => x.Seconds)) {
                    var taskThread = new TaskThread {
                        Seconds = scheduleTaskGrouped.Key
                    };
                    foreach(var scheduleTask in scheduleTaskGrouped) {
                        taskThread.AddTask(scheduleTask);
                    }
                    this._taskThreads.Add(taskThread);
                }
            }
        }

        /// <summary>
        /// Starts the task manager
        /// </summary>
        public void Start() {
            foreach(var taskThread in this._taskThreads) {
                taskThread.Start();
            }
        }

        /// <summary>
        /// Stops the task manager
        /// </summary>
        public void Stop() {
            foreach(var taskThread in this._taskThreads) {
                taskThread.Dispose();
            }
        }

        /// <summary>
        /// Gets the task mamanger instance
        /// </summary>
        public static TaskManager Instance {
            get {
                return _taskManager;
            }
        }

        /// <summary>
        /// Gets a list of task threads of this task manager
        /// </summary>
        public IList<TaskThread> TaskThreads {
            get {
                return new ReadOnlyCollection<TaskThread>(this._taskThreads);
            }
        }
    }
}