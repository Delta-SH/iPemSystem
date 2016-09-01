using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial interface IRoomKeyService {
        IPagedList<RoomKey> GetAllKeys(int pageIndex = 0, int pageSize = int.MaxValue);

        List<RoomKey> GetAllKeysAsList();
    }
}