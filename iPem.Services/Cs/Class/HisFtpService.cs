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

        private readonly IHisFtpRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisFtpService(
            IHisFtpRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<HisFtp> GetEvents(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisFtp>(this.GetEventsAsList(start, end), pageIndex, pageSize);
        }

        public List<HisFtp> GetEventsAsList(DateTime start, DateTime end) {
            return _hisRepository.GetEntities(start, end);
        }

        public IPagedList<HisFtp> GetEvents(DateTime start, DateTime end, EnmFtpEvent type, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisFtp>(this.GetEventsAsList(start, end, type), pageIndex, pageSize);
        }

        public List<HisFtp> GetEventsAsList(DateTime start, DateTime end, EnmFtpEvent type) {
            return _hisRepository.GetEntities(start, end, type);
        }

        #endregion

    }
}
