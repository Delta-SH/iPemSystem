using iPem.Core;
using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    /// <summary>
    /// IDeviceService interface
    /// </summary>
    public partial interface IDeviceService {
        Device GetDevice(string id);

        IPagedList<Device> GetDevices(string parent, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Device> GetAllDevices(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
