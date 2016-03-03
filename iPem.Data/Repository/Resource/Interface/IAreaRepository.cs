using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// Area repository interface
    /// </summary>
    public partial interface IAreaRepository {

        Area GetEntity(string id);

        List<Area> GetEntities();

    }
}
