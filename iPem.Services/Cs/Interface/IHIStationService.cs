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
        /// 获得所有的站点
        /// </summary>
        List<H_IStation> GetStations(DateTime date);

        /// <summary>
        /// 获得所有的站点(分页)
        /// </summary>
        IPagedList<H_IStation> GetPagedStations(DateTime date, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
