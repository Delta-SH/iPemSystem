using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
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
            return new PagedList<Supplier>(this.GetAllSuppliersAsList(), pageIndex, pageSize);
        }

        public List<Supplier> GetAllSuppliersAsList() {
            return _supplierRepository.GetEntities();
        }

        #endregion

    }
}
