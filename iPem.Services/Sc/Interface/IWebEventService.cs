using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 系统日志API
    /// </summary>
    public partial interface IWebEventService {
        /// <summary>
        /// 判断日志有效性
        /// </summary>
        bool IsEnabled(EnmEventLevel level);

        /// <summary>
        /// 获得指定时间内的系统日志
        /// </summary>
        IPagedList<H_WebEvent> GetWebEvents(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获得指定类型的系统日志
        /// </summary>
        IPagedList<H_WebEvent> GetWebEvents(DateTime start, DateTime end, EnmEventLevel[] levels, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获得指定类型的系统日志
        /// </summary>
        IPagedList<H_WebEvent> GetWebEvents(DateTime start, DateTime end, EnmEventType[] types, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获得指定类型的系统日志
        /// </summary>
        IPagedList<H_WebEvent> GetWebEvents(DateTime start, DateTime end, EnmEventLevel[] levels, EnmEventType[] types, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 新增系统日志
        /// </summary>
        void Insert(params H_WebEvent[] events);

        /// <summary>
        /// 删除系统日志
        /// </summary>
        void Delete(params H_WebEvent[] events);

        /// <summary>
        /// 清理系统日志
        /// </summary>
        void Clear();

        /// <summary>
        /// 清理系统日志
        /// </summary>
        void Clear(DateTime start, DateTime end);
    }
}
