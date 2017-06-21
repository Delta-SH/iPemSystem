using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 区域信息API
    /// </summary>
    public partial interface IAreaService {
        /// <summary>
        /// 获得指定的区域信息
        /// </summary>
        A_Area GetArea(string id);

        /// <summary>
        /// 获得所有的区域信息
        /// </summary>
        List<A_Area> GetAreas();

        /// <summary>
        /// 获得所有的区域信息（分页）
        /// </summary>
        IPagedList<A_Area> GetPagedAreas(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}