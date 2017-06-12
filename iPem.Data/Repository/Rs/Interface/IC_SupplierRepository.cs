using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 供应商表
    /// </summary>
    public partial interface IC_SupplierRepository {
        /// <summary>
        /// 获得指定的供应商
        /// </summary>
        C_Supplier GetSupplier(string id);

        /// <summary>
        /// 获得所有的供应商
        /// </summary>
        List<C_Supplier> GetSuppliers();
    }
}
