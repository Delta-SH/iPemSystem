using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 职位信息API
    /// </summary>
    public partial interface IDutyService {
        /// <summary>
        /// 获得指定的职位信息
        /// </summary>
        C_Duty GetDuty(string id);

        /// <summary>
        /// 获得所有的职位信息
        /// </summary>
        List<C_Duty> GetDuties();

        /// <summary>
        /// 获得所有的职位信息（分页）
        /// </summary>
        IPagedList<C_Duty> GetPagedDuties(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
