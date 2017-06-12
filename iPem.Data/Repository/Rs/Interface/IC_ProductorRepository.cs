using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 生产厂家表
    /// </summary>
    public partial interface IC_ProductorRepository {
        /// <summary>
        /// 获得指定的生产厂家表
        /// </summary>
        C_Productor GetProductor(string id);

        /// <summary>
        /// 获得所有的生产厂家表
        /// </summary>
        List<C_Productor> GetProductors();
    }
}
