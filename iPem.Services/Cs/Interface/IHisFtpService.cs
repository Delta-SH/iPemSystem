using iPem.Core;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisFtpService {

        IPagedList<HisFtp> GetEvents(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisFtp> GetEventsAsList(DateTime start, DateTime end);

        IPagedList<HisFtp> GetEvents(DateTime start, DateTime end, EnmFtpEvent type, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisFtp> GetEventsAsList(DateTime start, DateTime end, EnmFtpEvent type);

    }
}
