using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 资管接口区域API
    /// </summary>
    public partial interface IHIAreaService {
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
        /// <param name="parent">区域编号</param>
        List<H_IArea> GetAreasInParent(string parent);

        /// <summary>
        /// 获得所有的区域
        /// </summary>
        List<H_IArea> GetAreas();

        /// <summary>
        /// 获得所有的区域(分页)
        /// </summary>
        IPagedList<H_IArea> GetPagedAreas(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
