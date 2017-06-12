using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 标准信号表
    /// </summary>
    public partial interface IP_PointRepository {
        /// <summary>
        /// 获得指定设备下的信号
        /// </summary>
        List<P_Point> GetPointsInDevice(string id);

        /// <summary>
        /// 获得指定模版下的信号
        /// </summary>
        List<P_Point> GetPointsInProtocol(string protocol);

        /// <summary>
        /// 获得所有的信号
        /// </summary>
        List<P_Point> GetPoints();
    }
}
