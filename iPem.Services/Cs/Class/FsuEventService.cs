using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class FsuEventService : IFsuEventService {

        #region Fields

        private readonly IH_FsuEventRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FsuEventService(
            IH_FsuEventRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<H_FsuEvent> GetEventsInType(DateTime start, DateTime end, EnmFsuEvent type) {
            return _repository.GetEventsInType(start, end, type);
        }

        public List<H_FsuEvent> GetEvents(DateTime start, DateTime end) {
            return _repository.GetEvents(start, end);
        }

        public IPagedList<H_FsuEvent> GetPagedEvents(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_FsuEvent>(this.GetEvents(start, end), pageIndex, pageSize);
        }

        #endregion

    }
}
