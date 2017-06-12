using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class ProductorService : IProductorService {

        #region Fields

        private readonly IC_ProductorRepository _productorRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ProductorService(
            IC_ProductorRepository productorRepository,
            ICacheManager cacheManager) {
            this._productorRepository = productorRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_Productor GetProductor(string id) {
            return _productorRepository.GetEntity(id);
        }

        public IPagedList<C_Productor> GetAllProductors(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_Productor>(this.GetAllProductorsAsList(), pageIndex, pageSize);
        }

        public List<C_Productor> GetAllProductorsAsList() {
            return _productorRepository.GetEntities();
        }

        #endregion

    }
}