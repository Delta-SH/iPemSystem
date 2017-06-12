using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 机房类型表
    /// </summary>
    public partial interface IC_RoomTypeRepository {
        /// <summary>
        /// 获得指定的机房类型
        /// </summary>
        C_RoomType GetRoomType(string id);

        /// <summary>
        /// 获得所有的机房类型
        /// </summary>
        List<C_RoomType> GetRoomTypes();
    }
}
