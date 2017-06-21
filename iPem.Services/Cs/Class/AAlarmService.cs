using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
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

        public IPagedList<A_AAlarm> GetPagedAlarms(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_AAlarm>(this.GetAlarms(), pageIndex, pageSize);
        }

        public void Confirm(params A_AAlarm[] alarms) {
            if (alarms == null || alarms.Length == 0)
                throw new ArgumentNullException("alarms");

            if (_cacheManager.IsSet(GlobalCacheKeys.Global_ActiveAlarms))
                _cacheManager.Remove(GlobalCacheKeys.Global_ActiveAlarms);

            _repository.Confirm(alarms);
        }

        #endregion

    }
}
