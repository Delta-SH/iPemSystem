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
        /// 获得所有的区域
        /// </summary>
        List<H_IArea> GetAreas(DateTime date);

        /// <summary>
        /// 获得所有的区域(分页)
        /// </summary>
        IPagedList<H_IArea> GetPagedAreas(DateTime date, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
