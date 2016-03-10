using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    public partial interface IRoomTypeRepository {

        RoomType GetEntity(string id);

        List<RoomType> GetEntities();

    }
}
