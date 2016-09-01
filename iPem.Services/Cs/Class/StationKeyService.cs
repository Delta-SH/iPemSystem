using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class StationKeyService : IStationKeyService {

        #region Fields

        private readonly IStationKeyRepository _stationKeyRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public StationKeyService(
            IStationKeyRepository stationKeyRepository,
            ICacheManager cacheManager) {
            this._stationKeyRepository = stationKeyRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<StationKey> GetAllKeys(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<StationKey>(this.GetAllKeysAsList(), pageIndex, pageSize);
        }

        public List<StationKey> GetAllKeysAsList() {
            return _stationKeyRepository.GetEntities();
        }

        #endregion

    }
}
