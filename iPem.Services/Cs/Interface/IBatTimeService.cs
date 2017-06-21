using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 蓄电池后备时长API
    /// </summary>
    public partial interface IBatTimeService {
        /// <summary>
        /// 获得指定设备的蓄电池后备时长
        /// </summary>
        List<V_BatTime> GetValuesInDevice(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定设备下单信号的蓄电池后备时长
        /// </summary>
        List<V_BatTime> GetValuesInPoint(string device, string point, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的蓄电池后备时长
        /// </summary>
        List<V_BatTime> GetValues(DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的蓄电池后备时长(分页)
        /// </summary>
        IPagedList<V_BatTime> GetPagedValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
