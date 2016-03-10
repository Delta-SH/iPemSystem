using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
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
            List<StationType> stationTypes = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_StationTypesRepository)) {
                stationTypes = _cacheManager.Get<List<StationType>>(GlobalCacheKeys.Rs_StationTypesRepository);
            } else {
                stationTypes = _stationTypeRepository.GetEntities();
                _cacheManager.Set<List<StationType>>(GlobalCacheKeys.Rs_StationTypesRepository, stationTypes);
            }

            var result = new PagedList<StationType>(stationTypes, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}
