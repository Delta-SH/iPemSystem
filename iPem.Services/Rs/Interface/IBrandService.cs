using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 设备品牌API
    /// </summary>
    public partial interface IBrandService {
        /// <summary>
        /// 获得指定的设备品牌信息
        /// </summary>
        C_Brand GetBrand(string id);

        /// <summary>
        /// 获得指定的设备品牌信息
        /// </summary>
        List<C_Brand> GetBrands();

        /// <summary>
        /// 获得指定的设备品牌信息（分页）
        /// </summary>
        IPagedList<C_Brand> GetPagedBrands(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
