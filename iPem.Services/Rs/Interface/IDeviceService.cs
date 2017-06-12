using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IDeviceService {
        D_Device GetDevice(string id);

        IPagedList<D_Device> GetAllDevices(int pageIndex = 0, int pageSize = int.MaxValue);

        List<D_Device> GetAllDevicesAsList();

        IPagedList<D_Device> GetDevices(string parent, int pageIndex = 0, int pageSize = int.MaxValue);

        List<D_Device> GetDevicesAsList(string parent);
    }
}
