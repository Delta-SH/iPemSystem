using iPem.Core;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 断站、停电、发电API
    /// </summary>
    public partial interface ICutService {
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

        /// <summary>
        /// 获得所有的数据(分页)
        /// </summary>
        IPagedList<V_Cutting> GetPagedCuttings(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获得所有的数据(分页)
        /// </summary>
        IPagedList<V_Cuted> GetPagedCuteds(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
