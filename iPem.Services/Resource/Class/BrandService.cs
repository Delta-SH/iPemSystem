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
            List<Brand> brands = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_BrandsRepository)) {
                brands = _cacheManager.Get<List<Brand>>(GlobalCacheKeys.Rs_BrandsRepository);
            } else {
                brands = _brandRepository.GetEntities();
                _cacheManager.Set<List<Brand>>(GlobalCacheKeys.Rs_BrandsRepository, brands);
            }

            var result = new PagedList<Brand>(brands, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}
