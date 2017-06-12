using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HisBatTimeService : IHisBatTimeService {

        #region Fields

        private readonly IV_BatTimeRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisBatTimeService(
            IV_BatTimeRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<V_BatTime> GetHisBatTimes(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_BatTime>(this.GetHisBatTimesAsList(start, end), pageIndex, pageSize);
        }

        public List<V_BatTime> GetHisBatTimesAsList(DateTime start, DateTime end) {
            return _hisRepository.GetValues(start, end);
        }

        #endregion

    }
}
