using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 计量单位表
    /// </summary>
    public partial interface IC_UnitRepository {
        /// <summary>
        /// 获得指定的计量单位
        /// </summary>
        C_Unit GetUnit(string id);

        /// <summary>
        /// 获得所有的计量单位
        /// </summary>
        List<C_Unit> GetUnits();
    }
}
