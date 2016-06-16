using iPem.Core;
using iPem.Core.Domain.History;
using System;
using System.Collections.Generic;

namespace iPem.Services.History {
    public partial interface IHisStaticService {
        IPagedList<HisStatic> GetHisValues(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<HisStatic> GetHisValues(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<HisStatic> GetHisValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
