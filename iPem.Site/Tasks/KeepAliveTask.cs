using iPem.Core.Enum;
using iPem.Core.Task;
using iPem.Services.Sc;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using System;
using System.Net;

namespace iPem.Site.Tasks {
    /// <summary>
    /// Represents a task for keeping the site alive
    /// </summary>
    public class KeepAliveTask : ITask {

        #region Fields

        private int _seconds;
        private bool _runOnlyOnce;
        private int _order;
        private bool _enabled;

        #endregion

        #region Ctor

        public KeepAliveTask() {
            this._seconds = 60;
            this._runOnlyOnce = false;
            this._order = 0;
            this._enabled = true;
        }

        #endregion

        #region Methods

        public void Execute() {
            try {
                var local = EngineContext.Current.AppStore.Location;
                if(String.IsNullOrWhiteSpace(local))
                    return;

                var keepAliveUrl = string.Format("{0}keepalive", local);
                using(var wc = new WebClient()) {
                    wc.DownloadString(keepAliveUrl);
                }
            } catch(Exception exc) {
                var logger = EngineContext.Current.Resolve<IWebEventService>();
                logger.Error(EnmEventType.Exception, "", "#task.alive.keep", "KeepAliveTask Class", string.Format("Error while running the '{0}' schedule task. {1}", this.Name, exc.Message), exc);
            }
        }

        #endregion

        #region Properties

        public string Name {
            get { return "#task.alive.keep"; }
        }

        public int Seconds {
            get { return this._seconds; }
            set { this._seconds = value; }
        }

        public bool RunOnlyOnce {
            get { return this._runOnlyOnce; }
            set { this._runOnlyOnce = value; }
        }

        public int Order {
            get { return this._order; }
            set { this._order = value; }
        }

        public bool Enabled {
            get { return this._enabled; }
            set { this._enabled = value; }
        }

        #endregion

    }
}