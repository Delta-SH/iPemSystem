using iPem.Core;
using iPem.Core.Domain.Resource;
using System;

namespace iPem.Services.Resource {
    public partial interface IStationTypeService {

        StationType GetStationType(string id);

        IPagedList<StationType> GetAllStationTypes(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
