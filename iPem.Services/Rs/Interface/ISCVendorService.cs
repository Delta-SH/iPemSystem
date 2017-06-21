using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// SC厂家API
    /// </summary>
    public partial interface ISCVendorService {
        /// <summary>
        /// 获得指定的SC厂家
        /// </summary>
        C_SCVendor GetVendor(string id);

        /// <summary>
        /// 获得所有的SC厂家
        /// </summary>
        List<C_SCVendor> GetVendors();

        /// <summary>
        /// 获得所有的机房类型
        /// </summary>
        IPagedList<C_SCVendor> GetPagedVendors(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
