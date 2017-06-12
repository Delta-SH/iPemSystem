using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class StationService : IStationService {

        #region Fields

        private readonly IS_StationRepository _stationRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public StationService(
            IS_StationRepository stationRepository,
            ICacheManager cacheManager) {
            this._stationRepository = stationRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public S_Station GetStation(string id) {
            return _stationRepository.GetEntity(id);
        }

        public IPagedList<S_Station> GetAllStations(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<S_Station>(this.GetAllStationsAsList(), pageIndex, pageSize);
        }

        public List<S_Station> GetAllStationsAsList() {
            return _stationRepository.GetEntities();
        }

        public IPagedList<S_Station> GetStations(string parent, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<S_Station>(this.GetStationsAsList(parent), pageIndex, pageSize);
        }

        public List<S_Station> GetStationsAsList(string parent) {
            return _stationRepository.GetEntities(parent);
        }

        #endregion

    }
}
