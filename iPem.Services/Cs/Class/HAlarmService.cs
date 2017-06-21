using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HAlarmService : IHAlarmService {

        #region Fields

        private readonly IA_HAlarmRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HAlarmService(
            IA_HAlarmRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<A_HAlarm> GetAlarmsInArea(string id, DateTime start, DateTime end) {
            return _repository.GetAlarmsInArea(id, start, end);
        }

        public List<A_HAlarm> GetAlarmsInStation(string id, DateTime start, DateTime end) {
            return _repository.GetAlarmsInStation(id, start, end);
        }

        public List<A_HAlarm> GetAlarmsInRoom(string id, DateTime start, DateTime end) {
            return _repository.GetAlarmsInRoom(id, start, end);
        }

        public List<A_HAlarm> GetAlarmsInDevice(string id, DateTime start, DateTime end) {
            return _repository.GetAlarmsInDevice(id, start, end);
        }

        public List<A_HAlarm> GetAlarmsInPoint(string id, DateTime start, DateTime end) {
            return _repository.GetAlarmsInPoint(id, start, end);
        }

        public List<A_HAlarm> GetAlarms(DateTime start, DateTime end) {
            return _repository.GetAlarms(start, end);
        }

        public IPagedList<A_HAlarm> GetPagedAlarms(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_HAlarm>(this.GetAlarms(start, end), pageIndex, pageSize);
        }

        #endregion

    }
}
