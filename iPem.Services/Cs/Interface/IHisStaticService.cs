using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisStaticService {
        IPagedList<V_Static> GetValues(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_Static> GetValuesAsList(string device, DateTime start, DateTime end);

        IPagedList<V_Static> GetValues(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_Static> GetValuesAsList(string device, string point, DateTime start, DateTime end);

        IPagedList<V_Static> GetValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<V_Static> GetValuesAsList(DateTime start, DateTime end);
    }
}
