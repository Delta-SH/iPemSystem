using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class ParamDiffService : IParamDiffService {

        #region Fields

        private readonly IV_ParamDiffRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ParamDiffService(
            IV_ParamDiffRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<V_ParamDiff> GetDiffs(DateTime date) {
            return _repository.GetDiffs(date);
        }

        public IPagedList<V_ParamDiff> GetPagedDiffs(DateTime date, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_ParamDiff>(this.GetDiffs(date), pageIndex, pageSize);
        }

        #endregion
        
    }
}
