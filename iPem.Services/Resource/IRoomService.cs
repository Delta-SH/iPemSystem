using iPem.Core;
using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    /// <summary>
    /// IRoomService interface
    /// </summary>
    public partial interface IRoomService {
        Room GetRoom(string id);

        IPagedList<Room> GetRoomsByParent(string parent, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Room> GetAllRooms(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
