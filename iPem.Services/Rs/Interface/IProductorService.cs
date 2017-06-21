using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 生产厂家API
    /// </summary>
    public partial interface IProductorService {
        /// <summary>
        /// 获得指定的生产厂家表
        /// </summary>
        C_Productor GetProductor(string id);

        /// <summary>
        /// 获得所有的生产厂家表
        /// </summary>
        List<C_Productor> GetProductors();

        /// <summary>
        /// 获得所有的生产厂家表(分页)
        /// </summary>
        IPagedList<C_Productor> GetPagedProductors(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
