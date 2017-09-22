using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class SCVendorService : ISCVendorService {

        #region Fields

        private readonly IC_SCVendorRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public SCVendorService(
            IC_SCVendorRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_SCVendor GetVendor(string id) {
            return _repository.GetVendor(id);
        }

        public List<C_SCVendor> GetVendors() {
            var key = GlobalCacheKeys.Rs_SCVendorRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.Get<List<C_SCVendor>>(key);
            } else {
                var data = _repository.GetVendors();
                _cacheManager.Set(key, data);
                return data;
            }
        }

        public IPagedList<C_SCVendor> GetPagedVendors(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_SCVendor>(this.GetVendors(), pageIndex, pageSize);
        }

        #endregion

    }
}
