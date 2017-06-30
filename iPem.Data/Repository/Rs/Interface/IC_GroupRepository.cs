using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// SC组信息
    /// </summary>
    public partial interface IC_GroupRepository {
        /// <summary>
        /// 获得指定的SC组信息
        /// </summary>
        C_Group GetGroup(string id);

        /// <summary>
        /// 获得所有的SC组信息
        /// </summary>
        List<C_Group> GetGroups();
    }
}
