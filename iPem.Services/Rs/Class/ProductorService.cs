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

        private readonly IC_ProductorRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ProductorService(
            IC_ProductorRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_Productor GetProductor(string id) {
            return _repository.GetProductor(id);
        }

        public List<C_Productor> GetProductors() {
            return _repository.GetProductors();
        }

        public IPagedList<C_Productor> GetPagedProductors(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_Productor>(this.GetProductors(), pageIndex, pageSize);
        }

        #endregion

    }
}