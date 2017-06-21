using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class WebEventService : IWebEventService {

        #region Fields

        private readonly IH_WebEventRepository _repository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public WebEventService(IH_WebEventRepository repository) {
            this._repository = repository;
        }

        #endregion

        #region Methods

        public bool IsEnabled(EnmEventLevel level) {
            switch(level) {
                case EnmEventLevel.Debug:
                    return false;
                default:
                    return true;
            }
        }

        public IPagedList<H_WebEvent> GetWebEvents(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            var query = _repository.GetWebEvents(start, end);
            return new PagedList<H_WebEvent>(query, pageIndex, pageSize);
        }

        public IPagedList<H_WebEvent> GetWebEvents(DateTime start, DateTime end, EnmEventLevel[] levels, int pageIndex = 0, int pageSize = int.MaxValue) {
            var query = _repository.GetWebEvents(start, end, levels);
            return new PagedList<H_WebEvent>(query, pageIndex, pageSize);
        }

        public IPagedList<H_WebEvent> GetWebEvents(DateTime start, DateTime end, EnmEventType[] types, int pageIndex = 0, int pageSize = int.MaxValue) {
            var query = _repository.GetWebEvents(start, end, null, types);
            return new PagedList<H_WebEvent>(query, pageIndex, pageSize);
        }

        public IPagedList<H_WebEvent> GetWebEvents(DateTime start, DateTime end, EnmEventLevel[] levels, EnmEventType[] types, int pageIndex = 0, int pageSize = int.MaxValue) {
            var query = _repository.GetWebEvents(start, end, levels, types);
            return new PagedList<H_WebEvent>(query, pageIndex, pageSize);
        }

        public void Insert(params H_WebEvent[] events) {
            if (events == null || events.Length == 0)
                throw new ArgumentNullException("events");

            _repository.Insert(events);
        }

        public void Delete(params H_WebEvent[] events) {
            if (events == null || events.Length == 0)
                throw new ArgumentNullException("events");

            _repository.Delete(events);
        }

        public void Clear() {
            _repository.Clear();
        }

        public void Clear(DateTime start, DateTime end) {
            _repository.Clear(start, end);
        }

        #endregion

    }
}
