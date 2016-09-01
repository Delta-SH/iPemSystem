using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class AreaKeyService : IAreaKeyService {

        #region Fields

        private readonly IAreaKeyRepository _areaKeyRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AreaKeyService(
            IAreaKeyRepository areaKeyRepository,
            ICacheManager cacheManager) {
            this._areaKeyRepository = areaKeyRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<AreaKey> GetAllKeys(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<AreaKey>(this.GetAllKeysAsList(), pageIndex, pageSize);
        }

        public List<AreaKey> GetAllKeysAsList() {
            return _areaKeyRepository.GetEntities();
        }

        #endregion

    }
}
