using iPem.Core.Enum;
using iPem.Core.Task;
using iPem.Services.Master;
using iPem.Site.Extensions;
using iPem.Site.Infrastructure;
using System;
using System.Collections.Generic;

namespace iPem.Site.Tasks {
    /// <summary>
    /// Clear cache schedueled task implementation
    /// </summary>
    public class ClearCacheTask : ITask {

        #region Fields

        private int _seconds;
        private bool _runOnlyOnce;
        private int _order;
        private bool _enabled;

        #endregion

        #region Ctor

        public ClearCacheTask() {
            this._seconds = 60;
            this._runOnlyOnce = false;
            this._order = 0;
            this._enabled = true;
        }

        #endregion

        #region Methods

        public void Execute() {
            try {
                var keys = new List<Guid>(EngineContext.Current.WorkStores.Keys);
                foreach(var key in keys) {
                    if(EngineContext.Current.WorkStores[key].ExpireUtc < DateTime.UtcNow) {
                        EngineContext.Current.WorkStores.Remove(key);
                    }
                }
            } catch(Exception exc) {
                var logger = EngineContext.Current.Resolve<IWebLogger>();
                logger.Error(EnmEventType.Exception, "", "#task.cache.clear", "ClearCacheTask Class", string.Format("Error while running the '{0}' schedule task. {1}", this.Name, exc.Message), exc);
            }
        }

        #endregion

        #region Properties

        public string Name {
            get { return "#task.cache.clear"; }
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