using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 电池数据API
    /// </summary>
    public partial interface IBatService {
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

        /// <summary>
        /// 获得指定时间内的电池数据（分页）
        /// </summary>
        IPagedList<V_Bat> GetPagedValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}