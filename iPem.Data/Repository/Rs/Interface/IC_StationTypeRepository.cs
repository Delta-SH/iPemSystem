using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 站点类型表
    /// </summary>
    public partial interface IC_StationTypeRepository {
        /// <summary>
        /// 获得指定的站点类型
        /// </summary>
        C_StationType GetStationType(string id);

        /// <summary>
        /// 获得所有的站点类型
        /// </summary>
        List<C_StationType> GetStationTypes();
    }
}