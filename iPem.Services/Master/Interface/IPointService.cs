using iPem.Core;
using iPem.Core.Domain.Master;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    /// <summary>
    /// IPointService interface
    /// </summary>
    public partial interface IPointService {
        IPagedList<Point> GetPointsByType(EnmNode nodeType, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Point> GetPointsByProtcol(int protcol, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Point> GetPoints(int protcol, EnmNode nodeType, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Point> GetPoints(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
