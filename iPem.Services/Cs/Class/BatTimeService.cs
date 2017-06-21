using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class BatTimeService : IBatTimeService {

        #region Fields

        private readonly IV_BatTimeRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public BatTimeService(
            IV_BatTimeRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<V_BatTime> GetValuesInDevice(string id, DateTime start, DateTime end) {
            return _repository.GetValuesInDevice(id, start, end);
        }

        public List<V_BatTime> GetValuesInPoint(string device, string point, DateTime start, DateTime end) {
            return _repository.GetValuesInPoint(device, point, start, end);
        }

        public List<V_BatTime> GetValues(DateTime start, DateTime end) {
            return _repository.GetValues(start, end);
        }

        public IPagedList<V_BatTime> GetPagedValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_BatTime>(this.GetValues(start, end), pageIndex, pageSize);
        }

        #endregion
        
    }
}
