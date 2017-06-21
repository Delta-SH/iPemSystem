using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 自定义枚举表
    /// </summary>
    public partial interface IC_EnumMethodRepository {
        /// <summary>
        /// 获得指定编号的枚举定义
        /// </summary>
        C_EnumMethod GetEnumById(int id);

        /// <summary>
        /// 获得指定类型的枚举定义
        /// </summary>
        List<C_EnumMethod> GetEnumsByType(EnmMethodType type, string comment);

        /// <summary>
        /// 获得所有的枚举定义
        /// </summary>
        List<C_EnumMethod> GetEnums();
    }
}
