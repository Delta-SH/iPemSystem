using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HIAreaService : IHIAreaService {

        #region Fields

        private readonly IH_IAreaRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HIAreaService(
            IH_IAreaRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<H_IArea> GetAreas(DateTime date) {
            return _repository.GetAreas(date);
        }

        public IPagedList<H_IArea> GetPagedAreas(DateTime date, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_IArea>(this.GetAreas(date), pageIndex, pageSize);
        }

        #endregion
        
    }
}
