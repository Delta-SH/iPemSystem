using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 历史性能数据API
    /// </summary>
    public partial interface IHMeasureService {
        /// <summary>
        /// 获得指定区域(第三级区域)的性能数据
        /// </summary>
        List<V_HMeasure> GetMeasuresInArea(string area, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定站点的性能数据
        /// </summary>
        List<V_HMeasure> GetMeasuresInStation(string station, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定机房的性能数据
        /// </summary>
        List<V_HMeasure> GetMeasuresInRoom(string room, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定设备的性能数据
        /// </summary>
        List<V_HMeasure> GetMeasuresInDevice(string device, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定设备的性能数据
        /// </summary>
        List<V_HMeasure> GetMeasuresInPoints(string device, string[] points, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的性能数据
        /// </summary>
        List<V_HMeasure> GetMeasures(DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的性能数据（分页）
        /// </summary>
        IPagedList<V_HMeasure> GetPagedMeasures(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
