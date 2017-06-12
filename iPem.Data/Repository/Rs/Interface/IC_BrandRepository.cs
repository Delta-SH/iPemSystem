using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 设备品牌表
    /// </summary>
    public partial interface IC_BrandRepository {
        /// <summary>
        /// 获得指定的设备品牌信息
        /// </summary>
        C_Brand GetBrand(string id);

        /// <summary>
        /// 获得指定的设备品牌信息
        /// </summary>
        List<C_Brand> GetBrands();
    }
}
