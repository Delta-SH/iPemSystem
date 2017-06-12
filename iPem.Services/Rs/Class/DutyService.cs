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

        private readonly IC_DutyRepository _dutyRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DutyService(
            IC_DutyRepository dutyRepository,
            ICacheManager cacheManager) {
            this._dutyRepository = dutyRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_Duty GetDuty(string id) {
            return _dutyRepository.GetEntity(id);
        }

        public IPagedList<C_Duty> GetAllDuties(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_Duty>(this.GetAllDutiesAsList(), pageIndex, pageSize);
        }

        public List<C_Duty> GetAllDutiesAsList() {
            return _dutyRepository.GetEntities();
        }

        #endregion

    }
}
