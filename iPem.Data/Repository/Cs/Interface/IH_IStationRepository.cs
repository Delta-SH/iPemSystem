using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 资管接口站点表
    /// </summary>
    public partial interface IH_IStationRepository {
        /// <summary>
        /// 获得指定站点类型所包含的站点
        /// </summary>
        List<H_IStation> GetStationsInType(string type);

        /// <summary>
        /// 获得指定区域下所包含的站点
        /// </summary>
        /// <param name="parent">区域名称(第三级区域)</param>
        List<H_IStation> GetStationsInParent(string parent);

        /// <summary>
        /// 获得所有的站点
        /// </summary>
        List<H_IStation> GetStations();
    }
}
