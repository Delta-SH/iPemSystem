using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class OfflineService : IOfflineService {

        #region Fields

        private readonly IV_OfflineRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public OfflineService(
            IV_OfflineRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<V_Offline> GetActive() {
           return _repository.GetActive();
        }

        public List<V_Offline> GetActive(EnmFormula ftype) {
            return _repository.GetActive(ftype);
        }

        public List<V_Offline> GetActive(EnmSSH type, EnmFormula ftype) {
            return _repository.GetActive(type, ftype);
        }

        public List<V_Offline> GetHistory(DateTime start, DateTime end) {
            return _repository.GetHistory(start, end);
        }

        public List<V_Offline> GetHistory(EnmFormula ftype, DateTime start, DateTime end) {
            return _repository.GetHistory(ftype, start, end);
        }

        public List<V_Offline> GetHistory(EnmSSH type, EnmFormula ftype, DateTime start, DateTime end) {
            return _repository.GetHistory(type, ftype, start, end);
        }

        #endregion

    }
}
