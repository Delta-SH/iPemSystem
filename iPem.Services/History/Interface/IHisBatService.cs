using iPem.Core;
using iPem.Core.Domain.History;
using System;

namespace iPem.Services.History {
    public partial interface IHisBatService {

        IPagedList<HisBat> GetHisBats(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<HisBat> GetHisBats(string device, string point, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<HisBat> GetHisBats(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
