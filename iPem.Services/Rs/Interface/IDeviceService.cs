using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IDeviceService {
        Device GetDevice(string id);

        IPagedList<Device> GetAllDevices(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Device> GetAllDevicesAsList();

        IPagedList<Device> GetDevices(string parent, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Device> GetDevicesAsList(string parent);
    }
}
