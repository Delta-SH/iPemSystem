using iPem.Core;
using iPem.Core.Domain.History;
using System;

namespace iPem.Services.History {
    public partial interface IHisAlmService {

        IPagedList<HisAlm> GetHisAlms(string device, DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<HisAlm> GetHisAlms(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
