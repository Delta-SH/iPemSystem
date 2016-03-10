using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
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
            List<Productor> productors = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_ProductorsRepository)) {
                productors = _cacheManager.Get<List<Productor>>(GlobalCacheKeys.Rs_ProductorsRepository);
            } else {
                productors = _productorRepository.GetEntities();
                _cacheManager.Set<List<Productor>>(GlobalCacheKeys.Rs_ProductorsRepository, productors);
            }

            var result = new PagedList<Productor>(productors, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}