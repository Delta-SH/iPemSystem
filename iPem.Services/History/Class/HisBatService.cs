using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.History;
using iPem.Data.Repository.History;
using System;

namespace iPem.Services.History {
    public partial class HisBatService : IHisBatService {

        #region Fields

        private readonly IHisBatRepository _hisRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HisBatService(
            IHisBatRepository hisRepository,
            ICacheManager cacheManager) {
            this._hisRepository = hisRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<HisBat> GetHisBats(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _hisRepository.GetEntities(device, start, end);
            return new PagedList<HisBat>(result, pageIndex, pageSize);
        }

        public IPagedList<HisBat> GetHisBats(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _hisRepository.GetEntities(device, point, start, end);
            return new PagedList<HisBat>(result, pageIndex, pageSize);
        }

        public IPagedList<HisBat> GetHisBats(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _hisRepository.GetEntities(start, end);
            return new PagedList<HisBat>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
