using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
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

        public IPagedList<Station> GetAllStations(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Station> stations = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_StationsRepository)) {
                stations = _cacheManager.Get<List<Station>>(GlobalCacheKeys.Cs_StationsRepository);
            } else {
                stations = _stationRepository.GetEntities();
                _cacheManager.Set<List<Station>>(GlobalCacheKeys.Cs_StationsRepository, stations);
            }

            var result = new PagedList<Station>(stations, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}
