using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 站点信息表
    /// </summary>
    public partial interface IS_StationRepository {
        /// <summary>
        /// 获得指定的站点
        /// </summary>
        S_Station GetStation(string id);

        /// <summary>
        /// 获得指定区域(第三级区域)下的站点
        /// </summary>
        List<S_Station> GetStationsInArea(string id);

        /// <summary>
        /// 获得包含指定信号的站点
        /// 将CityElectNumber（市电路数）用来存储该站点下指定信号的数量
        /// </summary>
        List<S_Station> GetStationsWithPoints(IEnumerable<string> points);

        /// <summary>
        /// 获得所有的站点
        /// </summary>
        List<S_Station> GetStations();
    }
}
