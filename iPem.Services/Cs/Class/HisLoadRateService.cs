using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HisLoadRateService : IHisLoadRateService {

        #region Fields

        private readonly IHisLoadRateRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisLoadRateService(
            IHisLoadRateRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<HisLoadRate> GetHisLoadRates(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisLoadRate>(this.GetHisLoadRatesAsList(start, end), pageIndex, pageSize);
        }

        public List<HisLoadRate> GetHisLoadRatesAsList(DateTime start, DateTime end) {
            return _hisRepository.GetEntities(start, end);
        }

        public List<HisLoadRate> GetMaxLoadRates(DateTime start, DateTime end) {
            return _hisRepository.GetMaxEntities(start, end);
        }

        public List<HisLoadRate> GetMinLoadRates(DateTime start, DateTime end) {
            return _hisRepository.GetMinEntities(start, end);
        }

        #endregion

    }
}
