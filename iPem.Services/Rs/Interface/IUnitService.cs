using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 计量单位API
    /// </summary>
    public partial interface IUnitService {
        /// <summary>
        /// 获得指定的计量单位
        /// </summary>
        C_Unit GetUnit(string id);

        /// <summary>
        /// 获得所有的计量单位
        /// </summary>
        List<C_Unit> GetUnits();

        /// <summary>
        /// 获得所有的计量单位(分页)
        /// </summary>
        IPagedList<C_Unit> GetPagedUnits(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}