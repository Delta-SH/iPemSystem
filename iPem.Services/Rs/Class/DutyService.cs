using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class DutyService : IDutyService {

        #region Fields

        private readonly IC_DutyRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DutyService(
            IC_DutyRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_Duty GetDuty(string id) {
            return _repository.GetDuty(id);
        }

        public List<C_Duty> GetDuties() {
            return _repository.GetDuties();
        }

        public IPagedList<C_Duty> GetPagedDuties(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_Duty>(this.GetDuties(), pageIndex, pageSize);
        }

        #endregion

    }
}
