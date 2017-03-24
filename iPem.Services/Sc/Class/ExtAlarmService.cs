using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class ExtAlarmService : IExtAlarmService {

        #region Fields

        private readonly IExtAlarmRepository _extAlarmRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ExtAlarmService(
            IExtAlarmRepository extAlarmRepository,
            ICacheManager cacheManager) {
            this._extAlarmRepository = extAlarmRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<ExtAlarm> GetAllExtAlarms(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<ExtAlarm>(this.GetAllExtAlarmsAsList(), pageIndex, pageSize);
        }

        public List<ExtAlarm> GetAllExtAlarmsAsList() {
            return _extAlarmRepository.GetEntities();
        }

        public void Update(List<ExtAlarm> entities) {
            _extAlarmRepository.Update(entities);
        }

        public IPagedList<ExtAlarm> GetHisExtAlarms(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<ExtAlarm>(this.GetHisExtAlarmsAsList(start, end), pageIndex, pageSize);
        }

        public List<ExtAlarm> GetHisExtAlarmsAsList(DateTime start, DateTime end) {
            return _extAlarmRepository.GetHisEntities(start, end);
        }

        #endregion

    }
}
