using iPem.Core;
using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    /// <summary>
    /// IRoomService interface
    /// </summary>
    public partial interface IRoomService {
        IPagedList<Room> GetAllRooms(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
