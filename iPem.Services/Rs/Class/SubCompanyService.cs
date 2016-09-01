using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class SubCompanyService : ISubCompanyService {

        #region Fields

        private readonly ISubCompanyRepository _subCompanyRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public SubCompanyService(
            ISubCompanyRepository subCompanyRepository,
            ICacheManager cacheManager) {
            this._subCompanyRepository = subCompanyRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public SubCompany GetSubCompany(string id) {
            return _subCompanyRepository.GetEntity(id);
        }

        public IPagedList<SubCompany> GetAllSubCompanies(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<SubCompany>(this.GetAllSubCompaniesAsList(), pageIndex, pageSize);
        }

        public List<SubCompany> GetAllSubCompaniesAsList() {
            return _subCompanyRepository.GetEntities();
        }

        #endregion

    }
}
