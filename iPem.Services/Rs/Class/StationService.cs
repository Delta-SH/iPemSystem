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

        public IPagedList<Station> GetAllStations(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Station>(this.GetAllStationsAsList(), pageIndex, pageSize);
        }

        public List<Station> GetAllStationsAsList() {
            return _stationRepository.GetEntities();
        }

        public IPagedList<Station> GetStations(string parent, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Station>(this.GetStationsAsList(parent), pageIndex, pageSize);
        }

        public List<Station> GetStationsAsList(string parent) {
            return _stationRepository.GetEntities(parent);
        }

        #endregion

    }
}
