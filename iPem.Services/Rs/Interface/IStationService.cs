using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 站点信息API
    /// </summary>
    public partial interface IStationService {
        /// <summary>
        /// 获得指定的站点
        /// </summary>
        S_Station GetStation(string id);

        /// <summary>
        /// 获得指定区域(第三级区域)下的站点
        /// </summary>
        List<S_Station> GetStationsInArea(string parent);

        /// <summary>
        /// 获得所有的站点
        /// </summary>
        List<S_Station> GetStations();

        /// <summary>
        /// 获得所有的站点(分页)
        /// </summary>
        IPagedList<S_Station> GetPagedStations(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
