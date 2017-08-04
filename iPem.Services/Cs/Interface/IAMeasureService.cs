﻿using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 实时性能数据API
    /// </summary>
    public partial interface IAMeasureService {
        /// <summary>
        /// 获得指定设备下单信号的性能数据
        /// </summary>
        V_AMeasure GetMeasure(string device, string signalId, string signalNumber);

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
        /// 获得指定关键字多信号的性能数据
        /// </summary>
        List<V_AMeasure> GetMeasures(IList<ValuesPair<string, string, string>> keys);

        /// <summary>
        /// 获得所有的性能数据
        /// </summary>
        List<V_AMeasure> GetMeasures();

        /// <summary>
        /// 获得所有的性能数据(分页)
        /// </summary>
        IPagedList<V_AMeasure> GetPagedMeasures(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
