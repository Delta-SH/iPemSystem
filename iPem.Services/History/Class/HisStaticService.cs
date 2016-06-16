using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.History;
using iPem.Data.Repository.History;
using System;
using System.Collections.Generic;

namespace iPem.Services.History {
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

        public IPagedList<HisStatic> GetHisValues(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _hisRepository.GetEntities(device, start, end);
            return new PagedList<HisStatic>(result, pageIndex, pageSize);
        }

        public IPagedList<HisStatic> GetHisValues(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _hisRepository.GetEntities(device, point, start, end);
            return new PagedList<HisStatic>(result, pageIndex, pageSize);
        }

        public IPagedList<HisStatic> GetHisValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _hisRepository.GetEntities(start, end);
            return new PagedList<HisStatic>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
