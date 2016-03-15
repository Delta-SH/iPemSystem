using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial class DutyService : IDutyService {

        #region Fields

        private readonly IDutyRepository _dutyRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DutyService(
            IDutyRepository dutyRepository,
            ICacheManager cacheManager) {
            this._dutyRepository = dutyRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public Duty GetDuty(string id) {
            return _dutyRepository.GetEntity(id);
        }

        public IPagedList<Duty> GetAllDuties(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Duty> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_DutiesRepository)) {
                result = _cacheManager.Get<List<Duty>>(GlobalCacheKeys.Rs_DutiesRepository);
            } else {
                result = _dutyRepository.GetEntities();
                _cacheManager.Set<List<Duty>>(GlobalCacheKeys.Rs_DutiesRepository, result);
            }

            return new PagedList<Duty>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
