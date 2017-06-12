using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IRoomTypeService {
        C_RoomType GetRoomType(string id);

        IPagedList<C_RoomType> GetAllRoomTypes(int pageIndex = 0, int pageSize = int.MaxValue);

        List<C_RoomType> GetAllRoomTypesAsList();
    }
}
