using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class SubCompanyService : ISubCompanyService {

        #region Fields

        private readonly IC_SubCompanyRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public SubCompanyService(
            IC_SubCompanyRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_SubCompany GetCompany(string id) {
            return _repository.GetCompany(id);
        }

        public List<C_SubCompany> GetCompanies() {
            return _repository.GetCompanies();
        }

        public IPagedList<C_SubCompany> GetPagedCompanies(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_SubCompany>(this.GetCompanies(), pageIndex, pageSize);
        }

        #endregion

    }
}
