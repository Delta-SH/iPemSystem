using iPem.Core;
using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    /// <summary>
    /// IStationService interface
    /// </summary>
    public partial interface IStationService {
        Station GetStation(string id);

        IPagedList<Station> GetStationsInArea(string area, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Station> GetChildrenInStation(string parent, bool include = true, bool deep = true, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Station> GetAllStations(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
