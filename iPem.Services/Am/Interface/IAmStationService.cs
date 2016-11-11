using iPem.Core;
using iPem.Core.Domain.Am;
using System;
using System.Collections.Generic;

namespace iPem.Services.Am {
    public partial interface IAmStationService {
        IPagedList<AmStation> GetAmStations(string type, int pageIndex = 0, int pageSize = int.MaxValue);

        List<AmStation> GetAmStationsAsList(string type);

        IPagedList<AmStation> GetAmStations(int pageIndex = 0, int pageSize = int.MaxValue);

        List<AmStation> GetAmStationsAsList();
    }
}