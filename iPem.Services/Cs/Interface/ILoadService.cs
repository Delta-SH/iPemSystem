using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 开关电源带载率数据API
    /// </summary>
    public partial interface ILoadService {
        /// <summary>
        /// 获得指定设备的开关电源带载率数据
        /// </summary>
        List<V_Load> GetLoadsInDevice(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的开关电源带载率数据
        /// </summary>
        List<V_Load> GetLoads(DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的开关电源带载率数据（分页）
        /// </summary>
        IPagedList<V_Load> GetPagedLoads(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
