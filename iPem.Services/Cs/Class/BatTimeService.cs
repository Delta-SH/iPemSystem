using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class BatTimeService : IBatTimeService {

        #region Fields

        private readonly IV_BatTimeRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public BatTimeService(
            IV_BatTimeRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<V_BatTime> GetValues(DateTime start, DateTime end) {
            return _repository.GetValues(start, end);
        }

        public List<V_BatTime> GetValues(DateTime start, DateTime end, EnmBatStatus status) {
            return _repository.GetValues(start, end, status);
        }

        public List<V_BatTime> GetProcedures(DateTime start, DateTime end) {
            return _repository.GetProcedures(start, end);
        }

        public List<V_BatTime> GetProcedures(string device, DateTime start, DateTime end) {
            return _repository.GetProcedures(device, start, end);
        }

        #endregion
        
    }
}
