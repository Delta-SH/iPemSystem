using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 机房信息表
    /// </summary>
    public partial interface IS_RoomRepository {
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
    }
}
