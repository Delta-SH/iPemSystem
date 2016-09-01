using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    public partial interface IRoomKeyRepository {
        List<RoomKey> GetEntities();
    }
}
