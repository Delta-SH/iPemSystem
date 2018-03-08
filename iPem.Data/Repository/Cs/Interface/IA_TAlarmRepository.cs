using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 告警流水表
    /// </summary>
    public partial interface IA_TAlarmRepository {
        /// <summary>
        /// 获得指定的告警流水
        /// </summary>
        A_TAlarm GetEntity(long id);

        /// <summary>
        /// 获得所有的告警流水
        /// </summary>
        List<A_TAlarm> GetEntities();

        /// <summary>
        /// 新增告警流水
        /// </summary>
        void Insert(IEnumerable<A_TAlarm> entities);

        /// <summary>
        /// 删除告警流水
        /// </summary>
        void Delete(IEnumerable<A_TAlarm> entities);
    }
}
