using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// Station repository interface
    /// </summary>
    public partial interface IStationRepository {
        Station GetEntity(string id);

        List<Station> GetEntitiesInArea(string area);

        List<Station> GetChildrenEntities(string parent, bool include, bool deep);

        List<Station> GetEntities();
    }
}
