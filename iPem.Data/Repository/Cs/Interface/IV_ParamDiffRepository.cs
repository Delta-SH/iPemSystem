using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 参数自检数据表
    /// </summary>
    public partial interface IV_ParamDiffRepository {
        /// <summary>
        /// 获得指定月份的参数自检数据
        /// </summary>
        List<V_ParamDiff> GetDiffs(DateTime date);
    }
}
