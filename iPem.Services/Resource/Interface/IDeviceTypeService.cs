using iPem.Core;
using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial interface IDeviceTypeService {

        DeviceType GetDeviceType(int id);

        IPagedList<DeviceType> GetAllDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
