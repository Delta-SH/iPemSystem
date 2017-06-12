using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class SubCompanyService : ISubCompanyService {

        #region Fields

        private readonly IC_SubCompanyRepository _subCompanyRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public SubCompanyService(
            IC_SubCompanyRepository subCompanyRepository,
            ICacheManager cacheManager) {
            this._subCompanyRepository = subCompanyRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_SubCompany GetSubCompany(string id) {
            return _subCompanyRepository.GetEntity(id);
        }

        public IPagedList<C_SubCompany> GetAllSubCompanies(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_SubCompany>(this.GetAllSubCompaniesAsList(), pageIndex, pageSize);
        }

        public List<C_SubCompany> GetAllSubCompaniesAsList() {
            return _subCompanyRepository.GetEntities();
        }

        #endregion

    }
}
