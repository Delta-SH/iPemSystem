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

        private readonly IS_StationRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public StationService(
            IS_StationRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public S_Station GetStation(string id) {
            return _repository.GetStation(id);
        }

        public List<S_Station> GetStationsInArea(string id) {
            return _repository.GetStationsInArea(id);
        }

        public List<S_Station> GetStations() {
            return _repository.GetStations();
        }

        public IPagedList<S_Station> GetPagedStations(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<S_Station>(this.GetStations(), pageIndex, pageSize);
        }

        #endregion

    }
}
