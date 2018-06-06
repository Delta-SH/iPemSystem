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
        /// 获得所有的能耗数据
        /// </summary>
        List<V_Elec> GetActive();

        /// <summary>
        /// 获得指定的能耗数据
        /// </summary>
        List<V_Elec> GetActive(EnmSSH type);

        /// <summary>
        /// 获得指定的能耗数据
        /// </summary>
        List<V_Elec> GetActive(string id, EnmSSH type);

        /// <summary>
        /// 获得指定的能耗数据
        /// </summary>
        List<V_Elec> GetActive(EnmSSH type, EnmFormula formula);

        /// <summary>
        /// 获得历史能耗数据
        /// </summary>
        List<V_Elec> GetHistory(DateTime start, DateTime end);

        /// <summary>
        /// 获得指定的能耗数据
        /// </summary>
        List<V_Elec> GetHistory(EnmSSH type, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定的能耗数据
        /// </summary>
        List<V_Elec> GetHistory(EnmSSH type, EnmFormula formula, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定的能耗数据
        /// </summary>
        List<V_Elec> GetHistory(string id, EnmSSH type, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定的能耗数据
        /// </summary>
        List<V_Elec> GetHistory(string id, EnmSSH type, EnmFormula formula, DateTime start, DateTime end);

        /// <summary>
        /// 获得每天的指定能耗
        /// </summary>
        List<V_Elec> GetEachDay(string id, EnmSSH type, EnmFormula formula, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定能耗的总计
        /// </summary>
        double GetTotal(string id, EnmSSH type, EnmFormula formula, DateTime start, DateTime end);
    }
}
