using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// SC厂家表
    /// </summary>
    public partial interface IC_SCVendorRepository {
        /// <summary>
        /// 获得指定的SC厂家
        /// </summary>
        C_SCVendor GetVendor(string id);

        /// <summary>
        /// 获得所有的SC厂家
        /// </summary>
        List<C_SCVendor> GetVendors();
    }
}
