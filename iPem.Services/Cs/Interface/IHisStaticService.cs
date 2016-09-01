using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IHisStaticService {
        IPagedList<HisStatic> GetValues(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisStatic> GetValuesAsList(string device, DateTime start, DateTime end);

        IPagedList<HisStatic> GetValues(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisStatic> GetValuesAsList(string device, string point, DateTime start, DateTime end);

        IPagedList<HisStatic> GetValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<HisStatic> GetValuesAsList(DateTime start, DateTime end);
    }
}
