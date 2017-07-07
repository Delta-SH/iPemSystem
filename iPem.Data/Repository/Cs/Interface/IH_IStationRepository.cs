using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 资管接口站点表
    /// </summary>
    public partial interface IH_IStationRepository {
        /// <summary>
        /// 获得所有的站点
        /// </summary>
        List<H_IStation> GetStations(DateTime date);
    }
}
