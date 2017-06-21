using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 历史性能数据统计API
    /// </summary>
    public partial interface IStaticService {
        /// <summary>
        /// 获得指定设备的历史性能统计数据
        /// </summary>
        List<V_Static> GetValuesInDevice(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定设备下单信号的历史性能统计数据
        /// </summary>
        List<V_Static> GetValuesInPoint(string device, string point, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的历史性能统计数据
        /// </summary>
        List<V_Static> GetValues(DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的历史性能统计数据(分页)
        /// </summary>
        IPagedList<V_Static> GetPagedValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
