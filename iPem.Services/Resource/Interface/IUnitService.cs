using iPem.Core;
using iPem.Core.Domain.Resource;
using System;

namespace iPem.Services.Resource {
    public partial interface IUnitService {

        Unit GetUnit(string id);

        IPagedList<Unit> GetAllUnits(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}