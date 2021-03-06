﻿using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 停电、发电信息表
    /// </summary>
    public partial interface IV_OfflineRepository {
        /// <summary>
        /// 获取所有停电、发电数据
        /// </summary>
        List<V_Offline> GetActive();

        /// <summary>
        /// 获取指定的停电、发电数据
        /// </summary>
        List<V_Offline> GetActive(EnmFormula ftype);

        /// <summary>
        /// 获取指定的停电、发电数据
        /// </summary>
        List<V_Offline> GetActive(EnmSSH type, EnmFormula ftype);

        /// <summary>
        /// 获取所有停电、发电数据
        /// </summary>
        List<V_Offline> GetHistory(DateTime start, DateTime end);

        /// <summary>
        /// 获取指定的停电、发电数据
        /// </summary>
        List<V_Offline> GetHistory(EnmFormula ftype, DateTime start, DateTime end);

        /// <summary>
        /// 获取指定的停电、发电数据
        /// </summary>
        List<V_Offline> GetHistory(EnmSSH type, EnmFormula ftype, DateTime start, DateTime end);
    }
}
