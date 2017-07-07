using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 资管接口设备表
    /// </summary>
    public partial interface IH_IAreaRepository {
        /// <summary>
        /// 获得所有的区域
        /// </summary>
        List<H_IArea> GetAreas(DateTime date);
    }
}
