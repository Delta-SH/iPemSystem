using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class StationTypeService : IStationTypeService {

        #region Fields

        private readonly IC_StationTypeRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public StationTypeService(
            IC_StationTypeRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_StationType GetStationType(string id) {
            return _repository.GetStationType(id);
        }

        public List<C_StationType> GetStationTypes() {
            var key = GlobalCacheKeys.Rs_StationTypesRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.Get<List<C_StationType>>(key);
            } else {
                var data = _repository.GetStationTypes();
                _cacheManager.Set(key, data);
                return data;
            }
        }

        public IPagedList<C_StationType> GetPagedStationTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_StationType>(this.GetStationTypes(), pageIndex, pageSize);
        }

        #endregion

    }
}
