using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class LoadRateService : ILoadService {

        #region Fields

        private readonly IV_LoadRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public LoadRateService(
            IV_LoadRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<V_Load> GetLoadsInDevice(string id, DateTime start, DateTime end) {
            return _repository.GetLoadsInDevice(id, start, end);
        }

        public List<V_Load> GetLoadsInPoint(string device, string point, DateTime start, DateTime end) {
            return _repository.GetLoadsInPoint(device, point, start, end);
        }

        public List<V_Load> GetLoads(DateTime start, DateTime end) {
            return _repository.GetLoads(start, end);
        }

        public IPagedList<V_Load> GetPagedLoads(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Load>(this.GetLoads(start, end), pageIndex, pageSize);
        }

        #endregion
        
    }
}
