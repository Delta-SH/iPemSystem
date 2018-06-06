using iPem.Core;
using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 能耗数据API
    /// </summary>
    public partial interface IElecService {
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

        /// <summary>
        /// 获得当月指定能耗总计
        /// </summary>
        double GetCurrentMonthTotal(string id, EnmSSH type, EnmFormula formula);

        /// <summary>
        /// 获得上月指定能耗总计
        /// </summary>
        double GetLastMonthTotal(string id, EnmSSH type, EnmFormula formula);

        /// <summary>
        /// 获得当年指定能耗总计
        /// </summary>
        double GetCurrentYearTotal(string id, EnmSSH type, EnmFormula formula);

        /// <summary>
        /// 获得上年指定能耗总计
        /// </summary>
        double GetLastYearTotal(string id, EnmSSH type, EnmFormula formula);
    }
}
