using iPem.Core;
using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    /// <summary>
    /// IDeviceService interface
    /// </summary>
    public partial interface IDeviceService {
        IPagedList<Device> GetAllDevices(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
