using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 蓄电池充放电过程表
    /// </summary>
    public partial interface IV_BatTimeRepository {
        /// <summary>
        /// 获得指定设备的充放电过程
        /// </summary>
        List<V_BatTime> GetValuesInDevice(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定设备下单信号的充放电过程
        /// </summary>
        List<V_BatTime> GetValuesInPoint(string device, string point, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的充放电过程
        /// </summary>
        List<V_BatTime> GetValues(DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的充放电过程
        /// </summary>
        List<V_BatTime> GetProcedures(DateTime start, DateTime end);
    }
}