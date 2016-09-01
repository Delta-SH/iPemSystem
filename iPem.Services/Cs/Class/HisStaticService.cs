using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HisStaticService : IHisStaticService {

        #region Fields

        private readonly IHisStaticRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisStaticService(
            IHisStaticRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<HisStatic> GetValues(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisStatic>(this.GetValuesAsList(device, start, end), pageIndex, pageSize);
        }

        public List<HisStatic> GetValuesAsList(string device, DateTime start, DateTime end) {
            return _hisRepository.GetEntities(device, start, end);
        }

        public IPagedList<HisStatic> GetValues(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisStatic>(this.GetValuesAsList(device, point, start, end), pageIndex, pageSize);
        }

        public List<HisStatic> GetValuesAsList(string device, string point, DateTime start, DateTime end) {
            return _hisRepository.GetEntities(device, point, start, end);
        }

        public IPagedList<HisStatic> GetValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<HisStatic>(this.GetValuesAsList(start, end), pageIndex, pageSize);
        }

        public List<HisStatic> GetValuesAsList(DateTime start, DateTime end) {
            return _hisRepository.GetEntities(start, end);
        }

        #endregion

    }
}
