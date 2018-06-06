using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 电池充放电曲线API
    /// </summary>
    public partial interface IBatCurveService {
        /// <summary>
        /// 获得指定设备的电池充放电曲线
        /// </summary>
        List<V_BatCurve> GetProcedures(string device, int pack, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定设备的电池充放电曲线
        /// </summary>
        List<V_BatCurve> GetProcedures(string device, int pack, EnmBatPoint ptype, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定设备的电池曲线
        /// </summary>
        List<V_BatCurve> GetValues(string device, int pack, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定设备的电池曲线
        /// </summary>
        List<V_BatCurve> GetValues(string device, int pack, EnmBatPoint ptype, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的电池曲线
        /// </summary>
        List<V_BatCurve> GetValues(DateTime start, DateTime end);

        /// <summary>
        /// 获得每次充放电过程中的最小值
        /// </summary>
        List<V_BatCurve> GetMinValues(EnmBatStatus type, EnmBatPoint ptype, DateTime start, DateTime end);

        /// <summary>
        /// 获得每次充放电过程中的最大值
        /// </summary>
        List<V_BatCurve> GetMaxValues(EnmBatStatus type, EnmBatPoint ptype, DateTime start, DateTime end);
    }
}
