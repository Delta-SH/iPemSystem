using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// Device Status repository interface
    /// </summary>
    public partial interface IDeviceStatusRepository {

        DeviceStatus GetEntity(int id);

        List<DeviceStatus> GetEntities();

    }
}
