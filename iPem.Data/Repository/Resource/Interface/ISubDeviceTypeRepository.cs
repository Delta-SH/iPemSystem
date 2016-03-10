using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    public partial interface ISubDeviceTypeRepository {

        SubDeviceType GetEntity(int id);

        List<SubDeviceType> GetEntities();

    }
}
