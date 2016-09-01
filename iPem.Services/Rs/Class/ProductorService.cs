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

        private readonly IProductorRepository _productorRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ProductorService(
            IProductorRepository productorRepository,
            ICacheManager cacheManager) {
            this._productorRepository = productorRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public Productor GetProductor(string id) {
            return _productorRepository.GetEntity(id);
        }

        public IPagedList<Productor> GetAllProductors(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Productor>(this.GetAllProductorsAsList(), pageIndex, pageSize);
        }

        public List<Productor> GetAllProductorsAsList() {
            return _productorRepository.GetEntities();
        }

        #endregion

    }
}