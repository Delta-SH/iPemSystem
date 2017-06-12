using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HisLoadRateService : IHisLoadRateService {

        #region Fields

        private readonly IV_LoadRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisLoadRateService(
            IV_LoadRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<V_Load> GetHisLoadRates(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Load>(this.GetHisLoadRatesAsList(start, end), pageIndex, pageSize);
        }

        public List<V_Load> GetHisLoadRatesAsList(DateTime start, DateTime end) {
            return _hisRepository.GetEntities(start, end);
        }

        public List<V_Load> GetMaxInDevice(DateTime start, DateTime end, double max) {
            return _hisRepository.GetMaxInDevice(start, end, max);
        }

        #endregion

    }
}
