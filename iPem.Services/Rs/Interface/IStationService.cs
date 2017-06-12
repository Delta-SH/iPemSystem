using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IStationService {
        S_Station GetStation(string id);

        IPagedList<S_Station> GetAllStations(int pageIndex = 0, int pageSize = int.MaxValue);

        List<S_Station> GetAllStationsAsList();

        IPagedList<S_Station> GetStations(string parent, int pageIndex = 0, int pageSize = int.MaxValue);

        List<S_Station> GetStationsAsList(string parent);
    }
}
