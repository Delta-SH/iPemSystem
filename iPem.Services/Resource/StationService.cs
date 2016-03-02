using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial class StationService : IStationService {

        #region Fields

        private readonly IStationRepository _stationRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public StationService(
            IStationRepository stationRepository,
            ICacheManager cacheManager) {
            this._stationRepository = stationRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public Station GetStation(string id) {
            return _stationRepository.GetEntity(id);
        }

        public IPagedList<Station> GetStationsInArea(string area, int pageIndex = 0, int pageSize = int.MaxValue) {
            var key = string.Format(GlobalCacheKeys.Rs_StationsInAreaPattern, area);

            List<Station> stations = null;
            if(_cacheManager.IsSet(key)) {
                stations = _cacheManager.Get<List<Station>>(key);
            } else {
                stations = _stationRepository.GetEntitiesInArea(area);
                _cacheManager.Set<List<Station>>(key, stations);
            }

            var result = new PagedList<Station>(stations, pageIndex, pageSize);
            return result;
        }

        public IPagedList<Station> GetStationsInParent(string parent, int pageIndex = 0, int pageSize = int.MaxValue) {
            var key = string.Format(GlobalCacheKeys.Rs_StationsInParentPattern, parent);

            List<Station> stations = null;
            if(_cacheManager.IsSet(key)) {
                stations = _cacheManager.Get<List<Station>>(key);
            } else {
                stations = _stationRepository.GetEntitiesInParent(parent);
                _cacheManager.Set<List<Station>>(key, stations);
            }

            var result = new PagedList<Station>(stations, pageIndex, pageSize);
            return result;
        }

        public IPagedList<Station> GetAllStations(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Station> stations = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_StationsRepository)) {
                stations = _cacheManager.Get<List<Station>>(GlobalCacheKeys.Rs_StationsRepository);
            } else {
                stations = _stationRepository.GetEntities();
                _cacheManager.Set<List<Station>>(GlobalCacheKeys.Rs_StationsRepository, stations);
            }

            var result = new PagedList<Station>(stations, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}
