using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 实时性能数据表
    /// </summary>
    public partial interface IV_AMeasureRepository {
        /// <summary>
        /// 获得指定区域(第三级区域)的性能数据
        /// </summary>
        List<V_AMeasure> GetMeasuresInArea(string id);

        /// <summary>
        /// 获得指定站点的性能数据
        /// </summary>
        List<V_AMeasure> GetMeasuresInStation(string id);

        /// <summary>
        /// 获得指定机房的性能数据
        /// </summary>
        List<V_AMeasure> GetMeasuresInRoom(string id);

        /// <summary>
        /// 获得指定设备的性能数据
        /// </summary>
        List<V_AMeasure> GetMeasuresInDevice(string id);

        /// <summary>
        /// 获得指定设备下单信号的性能数据
        /// </summary>
        List<V_AMeasure> GetMeasuresInPoint(string device, string point);

        /// <summary>
        /// 获得所有的性能数据
        /// </summary>
        List<V_AMeasure> GetMeasures();
    }
}