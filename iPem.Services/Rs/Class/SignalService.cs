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

        public D_SimpleSignal GetSimpleSignal(string device, string point) {
            if (string.IsNullOrWhiteSpace(device)) throw new ArgumentNullException("device");
            if (string.IsNullOrWhiteSpace(point)) throw new ArgumentNullException("point");
            return _repository.GetSimpleSignal(device, point);
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

        public D_VSignal GetVSignal(string device, string point) {
            if (string.IsNullOrWhiteSpace(device)) throw new ArgumentNullException("device");
            if (string.IsNullOrWhiteSpace(point)) throw new ArgumentNullException("point");
            return _repository.GetVSignal(device, point);
        }

        public List<D_VSignal> GetVSignals() {
            return _repository.GetVSignals();
        }

        public List<D_VSignal> GetVSignals(IEnumerable<Kv<string, string>> pairs) {
            if (!pairs.Any()) return new List<D_VSignal>();
            var signals = pairs.DistinctBy(k => new { k.Key, k.Value });
            return _repository.GetVSignals(signals);
        }

        public List<D_VSignal> GetVSignals(string device) {
            if (string.IsNullOrWhiteSpace(device)) throw new ArgumentNullException("device");
            return _repository.GetVSignals(device);
        }

        public List<D_VSignal> GetVSignals(string device, bool _ai, bool _ao, bool _di, bool _do, bool _al) {
            var types = new List<EnmPoint>();
            if (_ai) types.Add(EnmPoint.AI);
            if (_ao) types.Add(EnmPoint.AO);
            if (_di) types.Add(EnmPoint.DI);
            if (_do) types.Add(EnmPoint.DO);
            if (_al) types.Add(EnmPoint.AL);

            if (!types.Contains(EnmPoint.AI)) return new List<D_VSignal>();
            return this.GetVSignals(device);
        }

        public List<D_VSignal> GetVSignals(string device, string[] names) {
            var signals = GetVSignals(device);
            if (names != null && names.Length > 0) {
                signals = signals.FindAll(s => CommonHelper.ConditionContain(s.Name, names));
            }

            return signals;
        }

        public List<D_VSignal> GetVSignals(string device, EnmVSignalCategory category) {
            if (string.IsNullOrWhiteSpace(device)) throw new ArgumentNullException("device");
            return _repository.GetVSignals(device, category);
        }

        public List<D_VSignal> GetVSignals(EnmVSignalCategory category) {
            return _repository.GetVSignals(category);
        }

        public void AddVSignals(params D_VSignal[] entities) {
            if (entities == null) throw new ArgumentNullException("entities");
            if (!entities.Any()) return;
            _repository.InsertVSignal(entities);
        }

        public void UpdateVSignals(params D_VSignal[] entities) {
            if (entities == null) throw new ArgumentNullException("entities");
            if (!entities.Any()) return;
            _repository.UpdateVSignal(entities);
        }

        public void RemoveVSignals(params D_VSignal[] entities) {
            if (entities == null) throw new ArgumentNullException("entities");
            if (!entities.Any()) return;
            _repository.DeleteVSignal(entities);
        }

        public List<D_SimpleSignal> GetAllSignals(string device) {
            if (string.IsNullOrWhiteSpace(device)) 
                throw new ArgumentNullException("device");

            var ssignals = GetSimpleSignalsInDevice(device);
            var vsignals = GetVSignals(device);
            foreach (var signal in vsignals) {
                ssignals.Add(new D_SimpleSignal {
                    DeviceId = signal.DeviceId,
                    PointId = signal.PointId,
                    Code = "virtual",
                    Number = "000",
                    PointType = signal.Type,
                    PointName = signal.Name,
                    OfficialName = signal.Name,
                    UnitState = signal.UnitState
                });
            }

            return ssignals;
        }

        public List<D_SimpleSignal> GetAllSignals(string device, bool _ai, bool _ao, bool _di, bool _do, bool _al) {
            if (string.IsNullOrWhiteSpace(device))
                throw new ArgumentNullException("device");

            var ssignals = GetSimpleSignalsInDevice(device, _ai, _ao, _di, _do, _al);
            var vsignals = GetVSignals(device, _ai, _ao, _di, _do, _al);
            foreach (var signal in vsignals) {
                ssignals.Add(new D_SimpleSignal {
                    DeviceId = signal.DeviceId,
                    PointId = signal.PointId,
                    Code = "virtual",
                    Number = "000",
                    PointType = signal.Type,
                    PointName = signal.Name,
                    OfficialName = signal.Name,
                    UnitState = signal.UnitState
                });
            }

            return ssignals;
        }

        public List<D_SimpleSignal> GetAllSignals(IEnumerable<Kv<string, string>> pairs) {
            var ssignals = GetSimpleSignals(pairs);
            var vsignals = GetVSignals(pairs);
            foreach (var signal in vsignals) {
                ssignals.Add(new D_SimpleSignal {
                    DeviceId = signal.DeviceId,
                    PointId = signal.PointId,
                    Code = "virtual",
                    Number = "000",
                    PointType = signal.Type,
                    PointName = signal.Name,
                    OfficialName = signal.Name,
                    UnitState = signal.UnitState
                });
            }

            return ssignals;
        }

        #endregion
        
    }
}
