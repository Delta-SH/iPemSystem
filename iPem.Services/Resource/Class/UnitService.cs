using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial class UnitService : IUnitService {

        #region Fields

        private readonly IUnitRepository _unitRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public UnitService(
            IUnitRepository unitRepository,
            ICacheManager cacheManager) {
            this._unitRepository = unitRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public Unit GetUnit(string id) {
            return _unitRepository.GetEntity(id);
        }

        public IPagedList<Unit> GetAllUnits(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Unit> units = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_UnitsRepository)) {
                units = _cacheManager.Get<List<Unit>>(GlobalCacheKeys.Rs_UnitsRepository);
            } else {
                units = _unitRepository.GetEntities();
                _cacheManager.Set<List<Unit>>(GlobalCacheKeys.Rs_UnitsRepository, units);
            }

            var result = new PagedList<Unit>(units, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}
