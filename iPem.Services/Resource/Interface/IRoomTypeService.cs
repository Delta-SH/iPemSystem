using iPem.Core;
using iPem.Core.Domain.Resource;
using System;

namespace iPem.Services.Resource {
    public partial interface IRoomTypeService {

        RoomType GetRoomType(string id);

        IPagedList<RoomType> GetAllRoomTypes(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
