using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// Device Type repository interface
    /// </summary>
    public partial interface IDeviceTypeRepository {

        DeviceType GetEntity(string id);

        List<DeviceType> GetEntities();

    }
}
