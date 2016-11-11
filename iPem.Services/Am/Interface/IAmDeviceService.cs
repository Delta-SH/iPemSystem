using iPem.Core;
using iPem.Core.Domain.Am;
using System;
using System.Collections.Generic;

namespace iPem.Services.Am {
    public partial interface IAmDeviceService {
        IPagedList<AmDevice> GetAmDevices(string type, int pageIndex = 0, int pageSize = int.MaxValue);

        List<AmDevice> GetAmDevicesAsList(string type);

        IPagedList<AmDevice> GetAmDevices(int pageIndex = 0, int pageSize = int.MaxValue);

        List<AmDevice> GetAmDevicesAsList();
    }
}
