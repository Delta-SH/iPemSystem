using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Room repository interface
    /// </summary>
    public partial interface IRoomRepository {
        List<Room> GetEntities();
    }
}
