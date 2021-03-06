﻿using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Repository.Cs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class AAlarmService : IAAlarmService {

        #region Fields

        private readonly IA_AAlarmRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AAlarmService(
            IA_AAlarmRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<A_AAlarm> GetAlarmsInArea(string id) {
            return _repository.GetAlarmsInArea(id);
        }

        public List<A_AAlarm> GetAlarmsInStation(string id) {
            return _repository.GetAlarmsInStation(id);
        }

        public List<A_AAlarm> GetAlarmsInRoom(string id) {
            return _repository.GetAlarmsInRoom(id);
        }

        public List<A_AAlarm> GetAlarmsInDevice(string id) {
            return _repository.GetAlarmsInDevice(id);
        }

        public List<A_AAlarm> GetAlarmsInSpan(DateTime start, DateTime end) {
            return _repository.GetAlarms(start, end);
        }

        public List<A_AAlarm> GetAlarms() {
            return _repository.GetAlarms();
        }

        public List<A_AAlarm> GetAllAlarmsInSpan(DateTime start, DateTime end) {
            return _repository.GetAllAlarms(start, end);
        }

        public List<A_AAlarm> GetAllAlarms() {
            return _repository.GetAllAlarms();
        }

        public List<A_AAlarm> GetPrimaryAlarms(string id) {
            return _repository.GetPrimaryAlarms(id);
        }

        public List<A_AAlarm> GetRelatedAlarms(string id) {
            return _repository.GetRelatedAlarms(id);
        }

        public List<A_AAlarm> GetFilterAlarms(string id) {
            return _repository.GetFilterAlarms(id);
        }

        public IPagedList<A_AAlarm> GetPagedAlarms(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_AAlarm>(this.GetAlarms(), pageIndex, pageSize);
        }

        public void Confirm(params A_AAlarm[] alarms) {
            if (alarms == null || alarms.Length == 0)
                throw new ArgumentNullException("alarms");

            _repository.Confirm(alarms);

            if (_cacheManager.IsSet(GlobalCacheKeys.Active_Alarms))
                _cacheManager.Remove(GlobalCacheKeys.Active_Alarms);

            if (_cacheManager.IsSet(GlobalCacheKeys.System_SC_Alarms))
                _cacheManager.Remove(GlobalCacheKeys.System_SC_Alarms);

            if (_cacheManager.IsSet(GlobalCacheKeys.System_FSU_Alarms))
                _cacheManager.Remove(GlobalCacheKeys.System_FSU_Alarms);
        }

        public A_AAlarm GetAlarm(string id) {
            return _repository.GetAlarm(id);
        }

        public void RemoveAlarms(params A_AAlarm[] alarms) {
            if (alarms == null) throw new ArgumentNullException("alarms");

            _repository.Delete(alarms);

            if (_cacheManager.IsSet(GlobalCacheKeys.Active_Alarms))
                _cacheManager.Remove(GlobalCacheKeys.Active_Alarms);

            if (_cacheManager.IsSet(GlobalCacheKeys.System_SC_Alarms))
                _cacheManager.Remove(GlobalCacheKeys.System_SC_Alarms);

            if (_cacheManager.IsSet(GlobalCacheKeys.System_FSU_Alarms))
                _cacheManager.Remove(GlobalCacheKeys.System_FSU_Alarms);
        }

        #endregion

    }
}
