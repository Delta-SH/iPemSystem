using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HisBatTimeService : IHisBatTimeService {

        #region Fields

        private readonly IHisBatTimeRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisBatTimeService(
            IHisBatTimeRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<HisBatTime> GetHisBatTimes(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisBatTime>(this.GetHisBatTimesAsList(start, end), pageIndex, pageSize);
        }

        public List<HisBatTime> GetHisBatTimesAsList(DateTime start, DateTime end) {
            return _hisRepository.GetEntities(start, end);
        }

        #endregion

    }
}
