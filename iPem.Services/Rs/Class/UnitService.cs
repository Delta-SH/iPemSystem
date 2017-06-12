using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class UnitService : IUnitService {

        #region Fields

        private readonly IC_UnitRepository _unitRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public UnitService(
            IC_UnitRepository unitRepository,
            ICacheManager cacheManager) {
            this._unitRepository = unitRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_Unit GetUnit(string id) {
            return _unitRepository.GetEntity(id);
        }

        public IPagedList<C_Unit> GetAllUnits(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_Unit>(this.GetAllUnitsAsList(), pageIndex, pageSize);
        }

        public List<C_Unit> GetAllUnitsAsList() {
            return _unitRepository.GetEntities();
        }

        #endregion

    }
}
