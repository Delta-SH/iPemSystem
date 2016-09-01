using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class ExtendAlmService : IExtendAlmService {

        #region Fields

        private readonly IExtendAlmRepository _extendAlmRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ExtendAlmService(
            IExtendAlmRepository extendAlmRepository,
            ICacheManager cacheManager) {
            this._extendAlmRepository = extendAlmRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<ExtAlm> GetAllExtAlms(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<ExtAlm>(this.GetAllExtAlmsAsList(), pageIndex, pageSize);
        }

        public List<ExtAlm> GetAllExtAlmsAsList() {
            return _extendAlmRepository.GetEntities();
        }

        public void Update(List<ExtAlm> entities) {
            _extendAlmRepository.Update(entities);
        }

        #endregion

    }
}
