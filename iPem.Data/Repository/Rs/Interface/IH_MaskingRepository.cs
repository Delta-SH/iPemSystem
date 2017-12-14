using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 配置同步表
    /// </summary>
    public partial interface IH_MaskingRepository {
        /// <summary>
        /// 获得所有的告警屏蔽记录
        /// </summary>
        List<H_Masking> GetEntities();
    }
}
