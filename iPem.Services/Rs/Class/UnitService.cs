using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
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
            return new PagedList<Unit>(this.GetAllUnitsAsList(), pageIndex, pageSize);
        }

        public List<Unit> GetAllUnitsAsList() {
            return _unitRepository.GetEntities();
        }

        #endregion

    }
}
