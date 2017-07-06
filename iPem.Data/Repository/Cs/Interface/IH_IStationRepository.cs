﻿using iPem.Core.Domain.Cs;
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
        List<H_IStation> GetStationsInTypeId(string type);

        /// <summary>
        /// 获得指定站点类型所包含的站点
        /// </summary>
        List<H_IStation> GetStationsInTypeName(string type);

        /// <summary>
        /// 获得指定区域下所包含的站点
        /// </summary>
        /// <param name="id">区域名称(第三级区域)</param>
        List<H_IStation> GetStationsInArea(string id);

        /// <summary>
        /// 获得所有的站点
        /// </summary>
        List<H_IStation> GetStations();
    }
}
