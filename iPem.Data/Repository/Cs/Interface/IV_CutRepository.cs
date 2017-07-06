using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 断站、停电、发电数据表
    /// </summary>
    public partial interface IV_CutRepository {
        /// <summary>
        /// 获取所有正在断站、停电、发电数据
        /// </summary>
        List<V_Cutting> GetCuttings();

        /// <summary>
        /// 获取指定类型的正在断站、停电、发电数据
        /// </summary>
        List<V_Cutting> GetCuttings(EnmCutType type);

        /// <summary>
        /// 获取指定时间的断站、停电、发电数据
        /// </summary>
        List<V_Cuted> GetCuteds(DateTime start, DateTime end);

        /// <summary>
        /// 获取指定类型的断站、停电、发电数据
        /// </summary>
        List<V_Cuted> GetCuteds(DateTime start, DateTime end, EnmCutType type);
    }
}
