using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 能耗数据表
    /// </summary>
    public partial interface IV_ElecRepository {
        /// <summary>
        /// 获得指定站点/机房的能耗数据
        /// </summary>
        List<V_Elec> GetValues(string id, EnmSSH type, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定站点/机房下不同类型的能耗数据
        /// </summary>
        List<V_Elec> GetValues(string id, EnmSSH type, EnmFormula formula, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定站点/机房下不同类型的能耗数据
        /// </summary>
        List<V_Elec> GetValues(EnmSSH type, EnmFormula formula, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定站点/机房下的能耗数据
        /// </summary>
        List<V_Elec> GetValues(EnmSSH type, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时间内的能耗数据
        /// </summary>
        List<V_Elec> GetValues(DateTime start, DateTime end);
    }
}
