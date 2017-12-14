using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 告警流水API
    /// </summary>
    public partial interface ITAlarmService {
        /// <summary>
        /// 获得指定的告警流水
        /// </summary>
        A_TAlarm GetAlarm(string fsuid, string serialno, EnmFlag alarmflag);

        /// <summary>
        /// 获得所有的告警流水
        /// </summary>
        List<A_TAlarm> GetAlarms();

        /// <summary>
        /// 新增告警流水
        /// </summary>
        void Add(params A_TAlarm[] alarms);

        /// <summary>
        /// 删除告警流水
        /// </summary>
        void Remove(params A_TAlarm[] alarms);
    }
}
