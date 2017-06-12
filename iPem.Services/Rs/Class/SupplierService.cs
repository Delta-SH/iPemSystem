using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class SupplierService : ISupplierService {

        #region Fields

        private readonly IC_SupplierRepository _supplierRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public SupplierService(
            IC_SupplierRepository supplierRepository,
            ICacheManager cacheManager) {
            this._supplierRepository = supplierRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_Supplier GetSupplier(string id) {
            return _supplierRepository.GetEntity(id);
        }

        public IPagedList<C_Supplier> GetAllSuppliers(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_Supplier>(this.GetAllSuppliersAsList(), pageIndex, pageSize);
        }

        public List<C_Supplier> GetAllSuppliersAsList() {
            return _supplierRepository.GetEntities();
        }

        #endregion

    }
}
