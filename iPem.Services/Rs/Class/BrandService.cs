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

        private readonly IC_BrandRepository _brandRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public BrandService(
            IC_BrandRepository brandRepository,
            ICacheManager cacheManager) {
            this._brandRepository = brandRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_Brand GetBrand(string id) {
            return _brandRepository.GetEntity(id);
        }

        public IPagedList<C_Brand> GetAllBrands(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_Brand>(this.GetAllBrandsAsList(), pageIndex, pageSize);
        }

        public List<C_Brand> GetAllBrandsAsList() {
            return _brandRepository.GetEntities();
        }

        #endregion

    }
}
