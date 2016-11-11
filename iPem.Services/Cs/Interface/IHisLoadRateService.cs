using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisLoadRateService {

        IPagedList<HisLoadRate> GetHisLoadRates(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisLoadRate> GetHisLoadRatesAsList(DateTime start, DateTime end);

        List<HisLoadRate> GetMaxLoadRates(DateTime start, DateTime end);

        List<HisLoadRate> GetMinLoadRates(DateTime start, DateTime end);
    }
}
