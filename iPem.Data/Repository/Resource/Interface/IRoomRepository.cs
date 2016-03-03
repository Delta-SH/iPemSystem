using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// Room repository interface
    /// </summary>
    public partial interface IRoomRepository {
        Room GetEntity(string id);

        List<Room> GetEntities(string parent);

        List<Room> GetEntities();
    }
}
