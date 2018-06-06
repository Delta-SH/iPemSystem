using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 历史性能数据表
    /// </summary>
    public partial interface IV_HMeasureRepository {
        /// <summary>
        /// 获得指定区域(第三级区域)的性能数据
        /// </summary>
        List<V_HMeasure> GetMeasuresInArea(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定站点的性能数据
        /// </summary>
        List<V_HMeasure> GetMeasuresInStation(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定机房的性能数据
        /// </summary>
        List<V_HMeasure> GetMeasuresInRoom(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定设备的性能数据
        /// </summary>
        List<V_HMeasure> GetMeasuresInDevice(string id, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定设备的性能数据
        /// </summary>
        List<V_HMeasure> GetMeasuresInPoints(string device, string[] points, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的性能数据
        /// </summary>
        List<V_HMeasure> GetMeasures(DateTime start, DateTime end);
    }
}
