using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 站点类型API
    /// </summary>
    public partial interface IStationTypeService {
        /// <summary>
        /// 获得指定的站点类型
        /// </summary>
        C_StationType GetStationType(string id);

        /// <summary>
        /// 获得所有的站点类型
        /// </summary>
        List<C_StationType> GetStationTypes();

        /// <summary>
        /// 获得所有的站点类型(分页)
        /// </summary>
        IPagedList<C_StationType> GetPagedStationTypes(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
