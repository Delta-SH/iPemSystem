using System;
using System.Collections.Generic;
using iPem.Core;
using iPem.Core.Enum;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;

namespace iPem.Services.Master {
    /// <summary>
    /// Web logger
    /// </summary>
    public partial class WebLogger : IWebLogger {

        #region Fields

        private readonly IWebEventRepository _webEventRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public WebLogger(IWebEventRepository webEventRepository) {
            this._webEventRepository = webEventRepository;
        }

        #endregion

        #region Methods

        public virtual bool IsEnabled(EnmEventLevel level) {
            switch(level) {
                case EnmEventLevel.Debug:
                    return false;
                default:
                    return true;
            }
        }

        public virtual IPagedList<WebEvent> GetAllLogs(DateTime startTime, DateTime endTime, int pageIndex = 0, int pageSize = int.MaxValue) {
            var query = _webEventRepository.GetEntities(startTime, endTime);
            return new PagedList<WebEvent>(query, pageIndex, pageSize);
        }

        public virtual IPagedList<WebEvent> GetAllLogs(DateTime startTime, DateTime endTime, EnmEventLevel[] levels, int pageIndex = 0, int pageSize = int.MaxValue) {
            var query = _webEventRepository.GetEntities(startTime, endTime, levels);
            return new PagedList<WebEvent>(query, pageIndex, pageSize);
        }

        public virtual IPagedList<WebEvent> GetAllLogs(DateTime startTime, DateTime endTime, EnmEventType[] types, int pageIndex = 0, int pageSize = int.MaxValue) {
            var query = _webEventRepository.GetEntities(startTime, endTime, null, types);
            return new PagedList<WebEvent>(query, pageIndex, pageSize);
        }

        public virtual IPagedList<WebEvent> GetAllLogs(DateTime startTime, DateTime endTime, EnmEventLevel[] levels, EnmEventType[] types, int pageIndex = 0, int pageSize = int.MaxValue) {
            var query = _webEventRepository.GetEntities(startTime, endTime, levels, types);
            return new PagedList<WebEvent>(query, pageIndex, pageSize);
        }

        public virtual void Insert(WebEvent log) {
            if (log == null)
                throw new ArgumentNullException("log");

            _webEventRepository.Insert(log);
        }

        public virtual void Insert(List<WebEvent> logs) {
            if (logs == null)
                throw new ArgumentNullException("logs");

            _webEventRepository.Insert(logs);
        }

        public virtual void Delete(WebEvent log) {
            if (log == null)
                throw new ArgumentNullException("log");

            _webEventRepository.Delete(log);
        }

        public virtual void Delete(List<WebEvent> logs) {
            if (logs == null)
                throw new ArgumentNullException("logs");

            _webEventRepository.Delete(logs);
        }

        public virtual void Clear() {
            _webEventRepository.Clear();
        }

        public virtual void Clear(DateTime startTime, DateTime endTime) {
            _webEventRepository.Clear(startTime, endTime);
        }

        #endregion

    }
}
