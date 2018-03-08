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
        /// 获得指定模版下的信号
        /// </summary>
        List<P_Point> GetPointsInProtocol(string id);

        /// <summary>
        /// 获得所有的信号
        /// </summary>
        List<P_Point> GetPoints();

        /// <summary>
        /// 获得指定信号和站点类型的信号参数
        /// </summary>
        P_SubPoint GetSubPoint(string point, string statype);

        /// <summary>
        /// 获得指定信号的信号参数
        /// 同一个信号在不同的站点类型下，有可能有不同的参数信息。
        /// </summary>
        List<P_SubPoint> GetSubPointsInPoint(string id);

        /// <summary>
        /// 获得所有的信号参数
        /// </summary>
        List<P_SubPoint> GetSubPoints();
    }
}
