using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using iPem.Data.Repository.Cs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class TAlarmService : ITAlarmService {

        #region Fields

        private readonly IA_TAlarmRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public TAlarmService(
            IA_TAlarmRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public A_TAlarm GetAlarm(string fsuid, string serialno, EnmFlag alarmflag) {
            return _repository.GetEntity(fsuid, serialno, alarmflag);
        }

        public List<A_TAlarm> GetAlarms() {
            return _repository.GetEntities();
        }

        public void Add(params A_TAlarm[] alarms) {
            _repository.Insert(alarms);

            if (_cacheManager.IsSet(GlobalCacheKeys.Active_Alarms))
                _cacheManager.Remove(GlobalCacheKeys.Active_Alarms);

            if (_cacheManager.IsSet(GlobalCacheKeys.System_Alarms))
                _cacheManager.Remove(GlobalCacheKeys.System_Alarms);
        }

        public void Remove(params A_TAlarm[] alarms) {
            _repository.Delete(alarms);

            if (_cacheManager.IsSet(GlobalCacheKeys.Active_Alarms))
                _cacheManager.Remove(GlobalCacheKeys.Active_Alarms);

            if (_cacheManager.IsSet(GlobalCacheKeys.System_Alarms))
                _cacheManager.Remove(GlobalCacheKeys.System_Alarms);
        }

        #endregion

    }
}
