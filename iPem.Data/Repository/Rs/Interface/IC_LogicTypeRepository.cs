using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 逻辑分类/逻辑子类信息表
    /// </summary>
    public partial interface IC_LogicTypeRepository {
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
        /// 获得指定逻辑分类下的所有逻辑子类
        /// </summary>
        List<C_SubLogicType> GetSubLogicTypes(string parent);

        /// <summary>
        /// 获得所有的逻辑子类
        /// </summary>
        List<C_SubLogicType> GetSubLogicTypes();
    }
}
