using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 自定义枚举API
    /// </summary>
    public partial interface IEnumMethodService {
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

        /// <summary>
        /// 获得所有的枚举定义（分页）
        /// </summary>
        IPagedList<C_EnumMethod> GetPagedEnums(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
