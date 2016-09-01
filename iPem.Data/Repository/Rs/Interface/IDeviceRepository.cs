using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface IDeviceRepository {
        Device GetEntity(string id);

        List<Device> GetEntities(string parent);

        List<Device> GetEntities();
    }
}
