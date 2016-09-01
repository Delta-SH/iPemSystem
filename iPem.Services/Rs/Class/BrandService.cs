using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
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
            return new PagedList<Brand>(this.GetAllBrandsAsList(), pageIndex, pageSize);
        }

        public List<Brand> GetAllBrandsAsList() {
            return _brandRepository.GetEntities();
        }

        #endregion

    }
}
