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

        private readonly IStationTypeRepository _stationTypeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public StationTypeService(
            IStationTypeRepository stationTypeRepository,
            ICacheManager cacheManager) {
            this._stationTypeRepository = stationTypeRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public StationType GetStationType(string id) {
            return _stationTypeRepository.GetEntity(id);
        }

        public IPagedList<StationType> GetAllStationTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<StationType>(this.GetAllStationTypesAsList(), pageIndex, pageSize);
        }

        public List<StationType> GetAllStationTypesAsList() {
            List<StationType> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_StationTypesRepository)) {
                result = _cacheManager.Get<List<StationType>>(GlobalCacheKeys.Rs_StationTypesRepository);
            } else {
                result = _stationTypeRepository.GetEntities();
                _cacheManager.Set<List<StationType>>(GlobalCacheKeys.Rs_StationTypesRepository, result);
            }

            return result;
        }

        #endregion

    }
}
