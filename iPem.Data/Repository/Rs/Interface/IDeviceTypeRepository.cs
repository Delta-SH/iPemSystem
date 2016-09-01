using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface IDeviceTypeRepository {

        DeviceType GetEntity(string id);

        SubDeviceType GetSubEntity(string id);

        List<DeviceType> GetEntities();

        List<SubDeviceType> GetSubEntities();

    }
}
