using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<S_Station> GetStationsWithPoints(IEnumerable<string> points) {
            return _repository.GetStationsWithPoints(points);
        }

        public List<S_Station> GetStations() {
            var key = GlobalCacheKeys.Rs_StationsRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetItemsFromList<S_Station>(key).ToList();
            } else {
                var data = _repository.GetStations();
                _cacheManager.AddItemsToList(key, data);
                return data;
            }
        }

        public IPagedList<S_Station> GetPagedStations(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<S_Station>(this.GetStations(), pageIndex, pageSize);
        }

        #endregion

    }
}
