using iPem.Core;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// Fsu操作日志API
    /// </summary>
    public partial interface IFsuEventService {
        /// <summary>
        /// 获得指定类型的FSU操作日志
        /// </summary>
        List<H_FsuEvent> GetEventsInType(DateTime start, DateTime end, EnmFsuEvent type);

        /// <summary>
        /// 获得指定时间内的FSU操作日志
        /// </summary>
        List<H_FsuEvent> GetEvents(DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的FSU操作日志(分页)
        /// </summary>
        IPagedList<H_FsuEvent> GetPagedEvents(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
