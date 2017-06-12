using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HisFtpService : IHisFtpService {

        #region Fields

        private readonly IH_FsuEventRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisFtpService(
            IH_FsuEventRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<H_FsuEvent> GetEvents(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_FsuEvent>(this.GetEventsAsList(start, end), pageIndex, pageSize);
        }

        public List<H_FsuEvent> GetEventsAsList(DateTime start, DateTime end) {
            return _hisRepository.GetEntities(start, end);
        }

        public IPagedList<H_FsuEvent> GetEvents(DateTime start, DateTime end, EnmFsuEvent type, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_FsuEvent>(this.GetEventsAsList(start, end, type), pageIndex, pageSize);
        }

        public List<H_FsuEvent> GetEventsAsList(DateTime start, DateTime end, EnmFsuEvent type) {
            return _hisRepository.GetEntities(start, end, type);
        }

        #endregion

    }
}
