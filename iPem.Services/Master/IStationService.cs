using iPem.Core;
using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    /// <summary>
    /// IStationService interface
    /// </summary>
    public partial interface IStationService {
        IPagedList<Station> GetAllStations(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
