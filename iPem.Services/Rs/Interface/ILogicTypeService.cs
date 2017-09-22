using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 逻辑分类/逻辑子类信息API
    /// </summary>
    public partial interface ILogicTypeService {
        /// <summary>
        /// 获得指定的逻辑分类
        /// </summary>
        C_LogicType GetLogicType(string id);

        /// <summary>
        /// 获得指定的逻辑子分类
        /// </summary>
        C_SubLogicType GetSubLogicType(string id);

        /// <summary>
        /// 获得所有的逻辑分类
        /// </summary>
        List<C_LogicType> GetLogicTypes();

        /// <summary>
        /// 获得所有的逻辑子类
        /// </summary>
        List<C_SubLogicType> GetSubLogicTypes();

        /// <summary>
        /// 获得所有的逻辑分类(分页)
        /// </summary>
        IPagedList<C_LogicType> GetPagedLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获得所有的逻辑子类(分页)
        /// </summary>
        IPagedList<C_SubLogicType> GetPagedSubLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
