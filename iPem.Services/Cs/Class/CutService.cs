using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPem.Services.Cs {
    public partial class CutService : ICutService {

        #region Fields

        private readonly IV_CutRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public CutService(
            IV_CutRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<V_Cutting> GetCuttings() {
            return _repository.GetCuttings();
        }

        public List<V_Cutting> GetCuttings(EnmCutType type) {
            return _repository.GetCuttings(type);
        }

        public List<V_Cuted> GetCuteds(DateTime start, DateTime end) {
            return _repository.GetCuteds(start, end);
        }

        public List<V_Cuted> GetCuteds(DateTime start, DateTime end, EnmCutType type) {
            return _repository.GetCuteds(start, end, type);
        }

        public IPagedList<V_Cutting> GetPagedCuttings(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Cutting>(this.GetCuttings(), pageIndex, pageSize);
        }

        public IPagedList<V_Cuted> GetPagedCuteds(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Cuted>(this.GetCuteds(start, end), pageIndex, pageSize);
        }

        #endregion
        
    }
}
