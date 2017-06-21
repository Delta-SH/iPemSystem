using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// Fsu操作日志表
    /// </summary>
    public partial interface IH_FsuEventRepository {
        /// <summary>
        /// 获得指定时段内的日志记录
        /// </summary>
        List<H_FsuEvent> GetEvents(DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时段内单个类型的日志记录
        /// </summary>
        List<H_FsuEvent> GetEventsInType(DateTime start, DateTime end, EnmFsuEvent type);
    }
}
