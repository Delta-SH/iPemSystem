using iPem.Core;
using iPem.Core.Domain.History;
using System;

namespace iPem.Services.History {
    public partial interface IHisValueService {
        IPagedList<HisValue> GetHisValues(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<HisValue> GetHisValues(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<HisValue> GetHisValues(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
