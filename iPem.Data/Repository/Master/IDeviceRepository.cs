using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Device repository interface
    /// </summary>
    public partial interface IDeviceRepository {
        List<Device> GetEntities();
    }
}
