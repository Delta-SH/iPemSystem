using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 资管接口设备表
    /// </summary>
    public partial interface IH_IAreaRepository {
        /// <summary>
        /// 获得指定区域类型所包含的区域
        /// </summary>
        List<H_IArea> GetAreasInTypeId(string type);

        /// <summary>
        /// 获得指定区域类型所包含的区域
        /// </summary>
        List<H_IArea> GetAreasInTypeName(string type);

        /// <summary>
        /// 获得指定区域下所包含的区域
        /// </summary>
        /// <param name="parent">站点编号</param>
        List<H_IArea> GetAreasInParent(string parent);

        /// <summary>
        /// 获得所有的区域
        /// </summary>
        List<H_IArea> GetAreas();
    }
}
