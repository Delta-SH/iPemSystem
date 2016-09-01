using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface IRoomTypeRepository {

        RoomType GetEntity(string id);

        List<RoomType> GetEntities();

    }
}
