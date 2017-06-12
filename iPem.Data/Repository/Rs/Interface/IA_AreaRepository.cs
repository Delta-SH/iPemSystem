using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 区域信息表
    /// </summary>
    public partial interface IA_AreaRepository {
        /// <summary>
        /// 获得指定的区域信息
        /// </summary>
        A_Area GetArea(string id);

        /// <summary>
        /// 获得所有的区域信息
        /// </summary>
        List<A_Area> GetAreas();
    }
}
