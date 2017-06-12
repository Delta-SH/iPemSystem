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

        private readonly IC_StationTypeRepository _stationTypeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public StationTypeService(
            IC_StationTypeRepository stationTypeRepository,
            ICacheManager cacheManager) {
            this._stationTypeRepository = stationTypeRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_StationType GetStationType(string id) {
            return _stationTypeRepository.GetEntity(id);
        }

        public IPagedList<C_StationType> GetAllStationTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_StationType>(this.GetAllStationTypesAsList(), pageIndex, pageSize);
        }

        public List<C_StationType> GetAllStationTypesAsList() {
            List<C_StationType> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_StationTypesRepository)) {
                result = _cacheManager.Get<List<C_StationType>>(GlobalCacheKeys.Rs_StationTypesRepository);
            } else {
                result = _stationTypeRepository.GetEntities();
                _cacheManager.Set<List<C_StationType>>(GlobalCacheKeys.Rs_StationTypesRepository, result);
            }

            return result;
        }

        #endregion

    }
}
