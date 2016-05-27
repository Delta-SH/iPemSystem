using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    /// <summary>
    /// Fsu service
    /// </summary>
    public partial class FsuService : IFsuService {

        #region Fields

        private readonly IFsuRepository _fsuRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FsuService(
            IFsuRepository fsuRepository,
            ICacheManager cacheManager) {
            this._fsuRepository = fsuRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public Fsu GetFsu(string id) {
            return _fsuRepository.GetEntity(id);
        }

        public IPagedList<Fsu> GetAllFsus(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Fsu> fsus = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_FsusRepository)) {
                fsus = _cacheManager.Get<List<Fsu>>(GlobalCacheKeys.Rs_FsusRepository);
            } else {
                fsus = _fsuRepository.GetEntities();
                _cacheManager.Set<List<Fsu>>(GlobalCacheKeys.Rs_FsusRepository, fsus);
            }

            return new PagedList<Fsu>(fsus, pageIndex, pageSize);
        }

        #endregion

    }
}
