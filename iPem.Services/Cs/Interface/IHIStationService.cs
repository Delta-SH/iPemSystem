using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 资管接口站点API
    /// </summary>
    public partial interface IHIStationService {
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

        /// <summary>
        /// 获得所有的站点(分页)
        /// </summary>
        IPagedList<H_IStation> GetPagedStations(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
