using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class FsuKeyService : IFsuKeyService {

        #region Fields

        private readonly IFsuKeyRepository _fsuKeyRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FsuKeyService(
            IFsuKeyRepository fsuKeyRepository,
            ICacheManager cacheManager) {
            this._fsuKeyRepository = fsuKeyRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<FsuKey> GetAllKeys(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<FsuKey>(this.GetAllKeysAsList(), pageIndex, pageSize);
        }

        public List<FsuKey> GetAllKeysAsList() {
            return _fsuKeyRepository.GetEntities();
        }

        #endregion

    }
}
