using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<D_SimpleSignal> GetSimpleSignals(IEnumerable<Kv<string, string>> pairs) {
            if (!pairs.Any()) return new List<D_SimpleSignal>();
            var signals = pairs.DistinctBy(k => new { k.Key, k.Value });
            return _repository.GetSimpleSignals(signals);
        }

        public List<D_SimpleSignal> GetSimpleSignalsInDevice(string device) {
            return _repository.GetSimpleSignalsInDevice(device);
        }

        public List<D_SimpleSignal> GetSimpleSignalsInDevice(string device, bool _ai, bool _ao, bool _di, bool _do, bool _al) {
            var types = new List<EnmPoint>();
            if (_ai) types.Add(EnmPoint.AI);
            if (_ao) types.Add(EnmPoint.AO);
            if (_di) types.Add(EnmPoint.DI);
            if (_do) types.Add(EnmPoint.DO);
            if (_al) types.Add(EnmPoint.AL);

            if (types.Count == 0) return new List<D_SimpleSignal>();
            return this.GetSimpleSignalsInDevice(device).FindAll(s => types.Contains(s.PointType));
        }

        public List<D_SimpleSignal> GetSimpleSignalsInDevices(IEnumerable<string> devices) {
            if (!devices.Any()) return new List<D_SimpleSignal>();
            return _repository.GetSimpleSignalsInDevices(devices.Distinct());
        }

        public List<D_SimpleSignal> GetSimpleSignalsInDevices(IEnumerable<string> devices, bool _ai, bool _ao, bool _di, bool _do, bool _al) {
            var types = new List<EnmPoint>();
            if (_ai) types.Add(EnmPoint.AI);
            if (_ao) types.Add(EnmPoint.AO);
            if (_di) types.Add(EnmPoint.DI);
            if (_do) types.Add(EnmPoint.DO);
            if (_al) types.Add(EnmPoint.AL);

            if (types.Count == 0) return new List<D_SimpleSignal>();
            return this.GetSimpleSignalsInDevices(devices).FindAll(s => types.Contains(s.PointType));
        }

        #endregion
        
    }
}
