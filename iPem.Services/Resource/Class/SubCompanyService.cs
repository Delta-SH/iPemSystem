using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
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
            List<SubCompany> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_SubCompaniesRepository)) {
                result = _cacheManager.Get<List<SubCompany>>(GlobalCacheKeys.Rs_SubCompaniesRepository);
            } else {
                result = _subCompanyRepository.GetEntities();
                _cacheManager.Set<List<SubCompany>>(GlobalCacheKeys.Rs_SubCompaniesRepository, result);
            }

            return new PagedList<SubCompany>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
