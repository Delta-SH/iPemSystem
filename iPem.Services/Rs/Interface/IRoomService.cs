using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IRoomService {
        S_Room GetRoom(string id);

        IPagedList<S_Room> GetAllRooms(int pageIndex = 0, int pageSize = int.MaxValue);

        List<S_Room> GetAllRoomsAsList();

        IPagedList<S_Room> GetRooms(string parent, int pageIndex = 0, int pageSize = int.MaxValue);

        List<S_Room> GetRoomsAsList(string parent);
    }
}
