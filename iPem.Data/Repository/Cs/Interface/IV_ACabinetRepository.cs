using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 列头柜数据表
    /// </summary>
    public partial interface IV_ACabinetRepository {
        /// <summary>
        /// 获得指定时间内的列头柜数据
        /// </summary>
        List<V_ACabinet> GetHistory(DateTime start, DateTime end);

        /// <summary>
        /// 获得指定分类的列头柜数据
        /// </summary>
        List<V_ACabinet> GetHistory(EnmVSignalCategory category, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定分类的列头柜数据
        /// </summary>
        List<V_ACabinet> GetHistory(string device, EnmVSignalCategory category, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定信号的列头柜数据
        /// </summary>
        List<V_ACabinet> GetHistory(string device, string point, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时段每个信号最后一条列头柜数据
        /// </summary>
        List<V_ACabinet> GetLast(string device, EnmVSignalCategory category, DateTime start, DateTime end);

        /// <summary>
        /// 获得指定时段每个信号最后一条列头柜数据
        /// </summary>
        List<V_ACabinet> GetLast(EnmVSignalCategory category, DateTime start, DateTime end);
    }
}
