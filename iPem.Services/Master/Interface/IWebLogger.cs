using System;
using System.Collections.Generic;
using iPem.Core;
using iPem.Core.Domain.Master;
using iPem.Core.Enum;

namespace iPem.Services.Master {
    /// <summary>
    /// Logger interface
    /// </summary>
    public partial interface IWebLogger {
        bool IsEnabled(EnmEventLevel level);

        IPagedList<WebEvent> GetAllLogs(DateTime startTime, DateTime endTime, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<WebEvent> GetAllLogs(DateTime startTime, DateTime endTime, EnmEventLevel[] levels, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<WebEvent> GetAllLogs(DateTime startTime, DateTime endTime, EnmEventType[] types, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<WebEvent> GetAllLogs(DateTime startTime, DateTime endTime, EnmEventLevel[] levels, EnmEventType[] types, int pageIndex = 0, int pageSize = int.MaxValue);

        void Insert(WebEvent log);

        void Delete(WebEvent log);

        void Delete(List<WebEvent> logs);

        void Clear();

        void Clear(DateTime startTime, DateTime endTime);
    }
}
