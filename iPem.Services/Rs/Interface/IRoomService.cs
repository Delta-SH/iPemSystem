using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IRoomService {
        Room GetRoom(string id);

        IPagedList<Room> GetAllRooms(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Room> GetAllRoomsAsList();

        IPagedList<Room> GetRooms(string parent, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Room> GetRoomsAsList(string parent);
    }
}
