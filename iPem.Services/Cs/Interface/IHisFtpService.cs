using iPem.Core;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisFtpService {

        IPagedList<H_FsuEvent> GetEvents(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<H_FsuEvent> GetEventsAsList(DateTime start, DateTime end);

        IPagedList<H_FsuEvent> GetEvents(DateTime start, DateTime end, EnmFsuEvent type, int pageIndex = 0, int pageSize = int.MaxValue);

        List<H_FsuEvent> GetEventsAsList(DateTime start, DateTime end, EnmFsuEvent type);

    }
}
