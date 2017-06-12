using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HisStaticService : IHisStaticService {

        #region Fields

        private readonly IV_StaticRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisStaticService(
            IV_StaticRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<V_Static> GetValues(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Static>(this.GetValuesAsList(device, start, end), pageIndex, pageSize);
        }

        public List<V_Static> GetValuesAsList(string device, DateTime start, DateTime end) {
            return _hisRepository.GetValuesInPointInDevice(device, start, end);
        }

        public IPagedList<V_Static> GetValues(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Static>(this.GetValuesAsList(device, point, start, end), pageIndex, pageSize);
        }

        public List<V_Static> GetValuesAsList(string device, string point, DateTime start, DateTime end) {
            return _hisRepository.GetValuesInPoint(device, point, start, end);
        }

        public IPagedList<V_Static> GetValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Static>(this.GetValuesAsList(start, end), pageIndex, pageSize);
        }

        public List<V_Static> GetValuesAsList(DateTime start, DateTime end) {
            return _hisRepository.GetValues(start, end);
        }

        #endregion

    }
}
