using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface IRoomRepository {
        Room GetEntity(string id);

        List<Room> GetEntities(string parent);

        List<Room> GetEntities();
    }
}
