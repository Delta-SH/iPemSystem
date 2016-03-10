using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    public partial interface IStationTypeRepository {

        StationType GetEntity(string id);

        List<StationType> GetEntities();

    }
}