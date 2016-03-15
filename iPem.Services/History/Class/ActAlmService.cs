using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.History;
using iPem.Data.Repository.History;
using System;

namespace iPem.Services.History {
    public partial class ActAlmService : IActAlmService {

        #region Fields

        private readonly IActAlmRepository _actRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ActAlmService(
            IActAlmRepository actRepository,
            ICacheManager cacheManager) {
            this._actRepository = actRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<ActAlm> GetActAlmsByDevice(string device, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _actRepository.GetEntities(device);
            return new PagedList<ActAlm>(result, pageIndex, pageSize);
        }

        public IPagedList<ActAlm> GetActAlmsByLevels(int[] levels, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _actRepository.GetEntities(levels);
            return new PagedList<ActAlm>(result, pageIndex, pageSize);
        }

        public IPagedList<ActAlm> GetActAlmsByTime(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _actRepository.GetEntities(start, end);
            return new PagedList<ActAlm>(result, pageIndex, pageSize);
        }

        public IPagedList<ActAlm> GetAllActAlms(int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _actRepository.GetEntities();
            return new PagedList<ActAlm>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
