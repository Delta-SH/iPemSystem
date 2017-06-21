using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 供应商API
    /// </summary>
    public partial interface ISupplierService {
        /// <summary>
        /// 获得指定的供应商
        /// </summary>
        C_Supplier GetSupplier(string id);

        /// <summary>
        /// 获得所有的供应商
        /// </summary>
        List<C_Supplier> GetSuppliers();

        /// <summary>
        /// 获得所有的供应商(分页)
        /// </summary>
        IPagedList<C_Supplier> GetPagedSuppliers(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}