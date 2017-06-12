using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IUnitService {
        C_Unit GetUnit(string id);

        IPagedList<C_Unit> GetAllUnits(int pageIndex = 0, int pageSize = int.MaxValue);

        List<C_Unit> GetAllUnitsAsList();
    }
}