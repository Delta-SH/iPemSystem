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

        private readonly IC_BrandRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public BrandService(
            IC_BrandRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_Brand GetBrand(string id) {
            return _repository.GetBrand(id);
        }

        public List<C_Brand> GetBrands() {
            return _repository.GetBrands();
        }

        public IPagedList<C_Brand> GetPagedBrands(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_Brand>(this.GetBrands(), pageIndex, pageSize);
        }

        #endregion

    }
}
