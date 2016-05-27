using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.History;
using iPem.Data.Repository.History;
using System;

namespace iPem.Services.History {
    public partial class HisAlmService : IHisAlmService {

        #region Fields

        private readonly IHisAlmRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisAlmService(
            IHisAlmRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<HisAlm> GetHisAlms(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _hisRepository.GetEntities(device, start, end);
            return new PagedList<HisAlm>(result, pageIndex, pageSize);
        }

        public IPagedList<HisAlm> GetHisAlms(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _hisRepository.GetEntities(start, end);
            return new PagedList<HisAlm>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
