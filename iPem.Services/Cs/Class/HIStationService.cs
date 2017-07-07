using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HIStationService : IHIStationService {

        #region Fields

        private readonly IH_IStationRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HIStationService(
            IH_IStationRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<H_IStation> GetStations(DateTime date) {
            return _repository.GetStations(date);
        }

        public IPagedList<H_IStation> GetPagedStations(DateTime date, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_IStation>(this.GetStations(date), pageIndex, pageSize);
        }

        #endregion

    }
}
