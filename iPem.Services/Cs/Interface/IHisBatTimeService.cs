using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisBatTimeService {

        IPagedList<HisBatTime> GetHisBatTimes(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisBatTime> GetHisBatTimesAsList(DateTime start, DateTime end);

        List<HisBatTime> GetMaxBatTimes(DateTime start, DateTime end);

        List<HisBatTime> GetMinBatTimes(DateTime start, DateTime end);

    }
}
