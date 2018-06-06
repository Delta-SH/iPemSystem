using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 蓄电池充放电过程表
    /// </summary>
    public partial interface IV_BatTimeRepository {
        /// <summary>
        /// 获得指定的充放电过程
        /// </summary>
        List<V_BatTime> GetValues(DateTime start, DateTime end);

        /// <summary>
        /// 获得指定的充放电过程
        /// </summary>
        List<V_BatTime> GetValues(DateTime start, DateTime end, EnmBatStatus type);

        /// <summary>
        /// 获得指定时间内的充放电过程
        /// </summary>
        List<V_BatTime> GetProcedures(DateTime start, DateTime end);

        /// <summary>
        /// 获得指定设备的充放电过程
        /// </summary>
        List<V_BatTime> GetProcedures(string device, DateTime start, DateTime end);
    }
}