using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 机房类型API
    /// </summary>
    public partial interface IRoomTypeService {
        /// <summary>
        /// 获得指定的机房类型
        /// </summary>
        C_RoomType GetRoomType(string id);

        /// <summary>
        /// 获得所有的机房类型
        /// </summary>
        List<C_RoomType> GetRoomTypes();

        /// <summary>
        /// 获得所有的机房类型
        /// </summary>
        IPagedList<C_RoomType> GetPagedRoomTypes(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
