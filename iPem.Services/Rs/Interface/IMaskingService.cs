using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 告警屏蔽记录API
    /// </summary>
    public partial interface IMaskingService {
        /// <summary>
        /// 获得所有的屏蔽告警记录
        /// </summary>
        List<H_Masking> GetMaskings();

        /// <summary>
        /// 获得所有的屏蔽告警HASH
        /// </summary>
        HashSet<string> GetHashMaskings();

        /// <summary>
        /// 获得所有的屏蔽告警记录(分页)
        /// </summary>
        IPagedList<H_Masking> GetPagedMaskings(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
