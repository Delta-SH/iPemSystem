using iPem.Core;
using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial interface IDeviceStatusService {

        DeviceStatus GetDeviceStatus(int id);

        IPagedList<DeviceStatus> GetAllDeviceStatus(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
