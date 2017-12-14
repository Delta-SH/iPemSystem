using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class SignalService : ISignalService {

        #region Fields

        private readonly ID_SignalRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public SignalService(
            ID_SignalRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<D_Signal> GetAbsThresholds() {
            return _repository.GetAbsThresholds();
        }

        public List<D_Signal> GetPerThresholds() {
            return _repository.GetPerThresholds();
        }

        public List<D_Signal> GetSavedPeriods() {
            return _repository.GetSavedPeriods();
        }

        public List<D_Signal> GetStorageRefTimes() {
            return _repository.GetStorageRefTimes();
        }

        public List<D_Signal> GetAlarmLimits() {
            return _repository.GetAlarmLimits();
        }

        public List<D_Signal> GetAlarmLevels() {
            return _repository.GetAlarmLevels();
        }

        public List<D_Signal> GetAlarmDelays() {
            return _repository.GetAlarmDelays();
        }

        public List<D_Signal> GetAlarmRecoveryDelays() {
            return _repository.GetAlarmRecoveryDelays();
        }

        public List<D_Signal> GetAlarmFilterings() {
            return _repository.GetAlarmFilterings();
        }

        public List<D_Signal> GetAlarmInferiors() {
            return _repository.GetAlarmInferiors();
        }

        public List<D_Signal> GetAlarmConnections() {
            return _repository.GetAlarmConnections();
        }

        public List<D_Signal> GetAlarmReversals() {
            return _repository.GetAlarmReversals();
        }

        #endregion
        
    }
}
