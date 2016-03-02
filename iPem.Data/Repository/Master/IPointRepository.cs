using iPem.Core.Domain.Master;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Point repository interface
    /// </summary>
    public partial interface IPointRepository {
        List<Point> GetEntities(EnmNode nodeType);

        List<Point> GetEntities(int protcol);

        List<Point> GetEntities(int protcol, EnmNode nodeType);

        List<Point> GetEntities();
    }
}
