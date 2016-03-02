using iPem.Core;
using iPem.Core.Domain.Resource;
using System;

namespace iPem.Services.Resource {
    /// <summary>
    /// IAreaService interface
    /// </summary>
    public partial interface IAreaService {

        Area GetArea(string id);

        IPagedList<Area> GetAreas(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}