using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Am;
using iPem.Data.Repository.Am;
using System;
using System.Collections.Generic;

namespace iPem.Services.Am {
    public partial class AmStationService : IAmStationService {

        #region Fields

        private readonly IAmStationRepository _amRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AmStationService(
            IAmStationRepository amRepository,
            ICacheManager cacheManager) {
            this._amRepository = amRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<AmStation> GetAmStations(string type, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<AmStation>(this.GetAmStationsAsList(type), pageIndex, pageSize);
        }

        public List<AmStation> GetAmStationsAsList(string type) {
            return _amRepository.GetEntities(type);
        }

        public IPagedList<AmStation> GetAmStations(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<AmStation>(this.GetAmStationsAsList(), pageIndex, pageSize);
        }

        public List<AmStation> GetAmStationsAsList() {
            return _amRepository.GetEntities();
        }

        #endregion

    }
}
