using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IAreaService {
        A_Area GetArea(string id);

        IPagedList<A_Area> GetAreas(int pageIndex = 0, int pageSize = int.MaxValue);

        List<A_Area> GetAreasAsList();
    }
}