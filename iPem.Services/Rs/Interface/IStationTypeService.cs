using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IStationTypeService {
        StationType GetStationType(string id);

        IPagedList<StationType> GetAllStationTypes(int pageIndex = 0, int pageSize = int.MaxValue);

        List<StationType> GetAllStationTypesAsList();
    }
}
