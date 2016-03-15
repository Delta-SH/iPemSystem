using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// Station repository interface
    /// </summary>
    public partial interface IStationRepository {
        Station GetEntity(string id);

        List<Station> GetEntitiesInParent(string parent);

        List<Station> GetEntities();
    }
}
