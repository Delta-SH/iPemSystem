using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 系统日志表
    /// </summary>
    public partial interface IH_WebEventRepository {
        /// <summary>
        /// 获得指定时间内的系统日志
        /// </summary>
        List<H_WebEvent> GetWebEvents(DateTime start, DateTime end);

        /// <summary>
        /// 获得指定类型的系统日志
        /// </summary>
        List<H_WebEvent> GetWebEvents(DateTime? start = null, DateTime? end = null, EnmEventLevel[] levels = null, EnmEventType[] types = null);

        /// <summary>
        /// 新增系统日志
        /// </summary>
        void Insert(IList<H_WebEvent> entities);

        /// <summary>
        /// 删除系统日志
        /// </summary>
        void Delete(IList<H_WebEvent> entities);

        /// <summary>
        /// 清理系统日志
        /// </summary>
        void Clear(DateTime? start = null, DateTime? end = null);
    }
}