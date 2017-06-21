using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 机房信息API
    /// </summary>
    public partial interface IRoomService {
        /// <summary>
        /// 获得指定的机房
        /// </summary>
        S_Room GetRoom(string id);

        /// <summary>
        /// 获得指定的站点下的机房
        /// </summary>
        List<S_Room> GetRoomsInStation(string id);

        /// <summary>
        /// 获得所有的机房
        /// </summary>
        List<S_Room> GetRooms();

        /// <summary>
        /// 获得所有的机房(分页)
        /// </summary>
        IPagedList<S_Room> GetPagedRooms(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
