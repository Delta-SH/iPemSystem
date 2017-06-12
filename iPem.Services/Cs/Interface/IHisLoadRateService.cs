using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisLoadRateService {

        IPagedList<V_Load> GetHisLoadRates(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_Load> GetHisLoadRatesAsList(DateTime start, DateTime end);

        List<V_Load> GetMaxInDevice(DateTime start, DateTime end, double max);

    }
}
