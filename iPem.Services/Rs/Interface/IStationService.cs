using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IStationService {
        Station GetStation(string id);

        IPagedList<Station> GetAllStations(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Station> GetAllStationsAsList();

        IPagedList<Station> GetStations(string parent, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Station> GetStationsAsList(string parent);
    }
}
