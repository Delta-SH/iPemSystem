using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial class BrandService : IBrandService {

        #region Fields

        private readonly IBrandRepository _brandRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public BrandService(
            IBrandRepository brandRepository,
            ICacheManager cacheManager) {
            this._brandRepository = brandRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public Brand GetBrand(string id) {
            return _brandRepository.GetEntity(id);
        }

        public IPagedList<Brand> GetAllBrands(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Brand> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_BrandsRepository)) {
                result = _cacheManager.Get<List<Brand>>(GlobalCacheKeys.Rs_BrandsRepository);
            } else {
                result = _brandRepository.GetEntities();
                _cacheManager.Set<List<Brand>>(GlobalCacheKeys.Rs_BrandsRepository, result);
            }

            return new PagedList<Brand>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
