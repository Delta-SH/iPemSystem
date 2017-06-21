using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class SupplierService : ISupplierService {

        #region Fields

        private readonly IC_SupplierRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public SupplierService(
            IC_SupplierRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_Supplier GetSupplier(string id) {
            return _repository.GetSupplier(id);
        }

        public List<C_Supplier> GetSuppliers() {
            return _repository.GetSuppliers();
        }

        public IPagedList<C_Supplier> GetPagedSuppliers(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_Supplier>(this.GetSuppliers(), pageIndex, pageSize);
        }

        #endregion

    }
}
