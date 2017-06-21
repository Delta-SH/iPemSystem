using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class BatService : IBatService {

        #region Fields

        private readonly IV_BatRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public BatService(
            IV_BatRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<V_Bat> GetValuesInDevice(string device, DateTime start, DateTime end) {
            return _repository.GetValuesInDevice(device, start, end);
        }

        public List<V_Bat> GetValuesInPoint(string device, string point, DateTime start, DateTime end) {
            return _repository.GetValuesInPoint(device, point, start, end);
        }

        public List<V_Bat> GetValues(DateTime start, DateTime end) {
            return _repository.GetValues(start, end);
        }

        public IPagedList<V_Bat> GetPagedValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Bat>(this.GetValues(start, end), pageIndex, pageSize);
        }

        #endregion

    }
}
