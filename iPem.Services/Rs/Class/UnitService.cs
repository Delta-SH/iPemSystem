using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class UnitService : IUnitService {

        #region Fields

        private readonly IC_UnitRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public UnitService(
            IC_UnitRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_Unit GetUnit(string id) {
            return _repository.GetUnit(id);
        }

        public List<C_Unit> GetUnits() {
            return _repository.GetUnits();
        }

        public IPagedList<C_Unit> GetPagedUnits(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_Unit>(this.GetUnits(), pageIndex, pageSize);
        }

        #endregion

    }
}
