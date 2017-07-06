using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 自检参数API
    /// </summary>
    public partial interface IParamDiffService {
        /// <summary>
        /// 获得指定月份的参数自检数据
        /// </summary>
        List<V_ParamDiff> GetDiffs(DateTime date);

        /// <summary>
        /// 获得指定月份的参数自检数据(分页)
        /// </summary>
        IPagedList<V_ParamDiff> GetPagedDiffs(DateTime date, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
