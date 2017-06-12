using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 职位信息表
    /// </summary>
    public partial interface IC_DutyRepository {
        /// <summary>
        /// 获得指定的职位信息
        /// </summary>
        C_Duty GetDuty(string id);

        /// <summary>
        /// 获得所有的职位信息
        /// </summary>
        List<C_Duty> GetDuties();
    }
}
