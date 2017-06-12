using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 开关电源带载率数据表
    /// </summary>
    public partial interface IV_LoadRepository {
        /// <summary>
        /// 获得指定设备的开关电源带载率数据
        /// </summary>
        List<V_Load> GetLoadsInDevice(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定设备下单信号的开关电源带载率数据
        /// </summary>
        List<V_Load> GetLoadsInPoint(string device, string point, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的开关电源带载率数据
        /// </summary>
        List<V_Load> GetLoads(DateTime start, DateTime end);
    }
}
