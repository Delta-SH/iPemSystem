using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IStationTypeService {
        C_StationType GetStationType(string id);

        IPagedList<C_StationType> GetAllStationTypes(int pageIndex = 0, int pageSize = int.MaxValue);

        List<C_StationType> GetAllStationTypesAsList();
    }
}
