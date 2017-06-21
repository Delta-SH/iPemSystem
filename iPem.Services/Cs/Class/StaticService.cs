using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class StaticService : IStaticService {

        #region Fields

        private readonly IV_StaticRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public StaticService(
            IV_StaticRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<V_Static> GetValuesInDevice(string id, DateTime start, DateTime end) {
            return _repository.GetValuesInDevice(id, start, end);
        }

        public List<V_Static> GetValuesInPoint(string device, string point, DateTime start, DateTime end) {
            return _repository.GetValuesInPoint(device, point, start, end);
        }

        public List<V_Static> GetValues(DateTime start, DateTime end) {
            return _repository.GetValues(start, end);
        }

        public IPagedList<V_Static> GetPagedValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Static>(this.GetValues(start, end), pageIndex, pageSize);
        }

        #endregion

    }
}
