using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// Device repository interface
    /// </summary>
    public partial interface IDeviceRepository {
        Device GetEntity(string id);

        List<Device> GetEntities(string parent);

        List<Device> GetEntities();
    }
}
