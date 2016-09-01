using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IUnitService {
        Unit GetUnit(string id);

        IPagedList<Unit> GetAllUnits(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Unit> GetAllUnitsAsList();
    }
}