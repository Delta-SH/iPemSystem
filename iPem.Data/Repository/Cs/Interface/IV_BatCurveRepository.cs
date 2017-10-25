using iPem.Core;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 电池充放电曲线表
    /// </summary>
    public partial interface IV_BatCurveRepository {
        /// <summary>
        /// 获得指定设备的电池曲线数据
        /// </summary>
        List<V_BatCurve> GetProcedures(string device, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定信号的电池曲线数据
        /// </summary>
        List<V_BatCurve> GetProcedures(string device, string point, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定设备的电池曲线数据
        /// </summary>
        List<V_BatCurve> GetValues(string device, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定信号的电池曲线数据
        /// </summary>
        List<V_BatCurve> GetValues(string device, string point, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的电池曲线数据
        /// </summary>
        List<V_BatCurve> GetValues(DateTime start, DateTime end);
    }
}
