using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IWebLogger {
        bool IsEnabled(EnmEventLevel level);

        IPagedList<H_WebEvent> GetAllLogs(DateTime startTime, DateTime endTime, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<H_WebEvent> GetAllLogs(DateTime startTime, DateTime endTime, EnmEventLevel[] levels, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<H_WebEvent> GetAllLogs(DateTime startTime, DateTime endTime, EnmEventType[] types, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<H_WebEvent> GetAllLogs(DateTime startTime, DateTime endTime, EnmEventLevel[] levels, EnmEventType[] types, int pageIndex = 0, int pageSize = int.MaxValue);

        void Insert(H_WebEvent log);

        void Insert(List<H_WebEvent> logs);

        void Delete(H_WebEvent log);

        void Delete(List<H_WebEvent> logs);

        void Clear();

        void Clear(DateTime startTime, DateTime endTime);
    }
}
