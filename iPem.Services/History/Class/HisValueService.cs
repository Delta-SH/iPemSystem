using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.History;
using iPem.Data.Repository.History;
using System;

namespace iPem.Services.History {
    public partial class HisValueService : IHisValueService {

        #region Fields

        private readonly IHisValueRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisValueService(
            IHisValueRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<HisValue> GetHisValues(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _hisRepository.GetEntities(device, start, end);
            return new PagedList<HisValue>(result, pageIndex, pageSize);
        }

        public IPagedList<HisValue> GetHisValues(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _hisRepository.GetEntities(device, point, start, end);
            return new PagedList<HisValue>(result, pageIndex, pageSize);
        }

        public IPagedList<HisValue> GetHisValues(string[] points, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _hisRepository.GetEntities(points, start, end);
            return new PagedList<HisValue>(result, pageIndex, pageSize);
        }

        public IPagedList<HisValue> GetHisValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _hisRepository.GetEntities(start, end);
            return new PagedList<HisValue>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
