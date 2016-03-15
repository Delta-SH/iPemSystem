using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial class SupplierService : ISupplierService {

        #region Fields

        private readonly ISupplierRepository _supplierRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public SupplierService(
            ISupplierRepository supplierRepository,
            ICacheManager cacheManager) {
            this._supplierRepository = supplierRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public Supplier GetSupplier(string id) {
            return _supplierRepository.GetEntity(id);
        }

        public IPagedList<Supplier> GetAllSuppliers(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Supplier> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_SuppliersRepository)) {
                result = _cacheManager.Get<List<Supplier>>(GlobalCacheKeys.Rs_SuppliersRepository);
            } else {
                result = _supplierRepository.GetEntities();
                _cacheManager.Set<List<Supplier>>(GlobalCacheKeys.Rs_SuppliersRepository, result);
            }

            return new PagedList<Supplier>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
