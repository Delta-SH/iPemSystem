using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 电池数据表
    /// </summary>
    public partial interface IV_BatRepository {
        /// <summary>
        /// 获得指定设备的电池数据
        /// </summary>
        List<V_Bat> GetValuesInDevice(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定设备下单信号的电池数据
        /// </summary>
        List<V_Bat> GetValuesInPoint(string device, string point, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的电池数据
        /// </summary>
        List<V_Bat> GetValues(DateTime start, DateTime end);
    }
}