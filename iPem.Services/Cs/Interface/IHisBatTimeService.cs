using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisBatTimeService {

        IPagedList<V_BatTime> GetHisBatTimes(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_BatTime> GetHisBatTimesAsList(DateTime start, DateTime end);

    }
}
