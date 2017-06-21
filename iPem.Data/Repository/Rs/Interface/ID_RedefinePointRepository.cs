using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 信号重定义表
    /// </summary>
    public partial interface ID_RedefinePointRepository {
        /// <summary>
        /// 获得指定的重定义信号
        /// </summary>
        D_RedefinePoint GetRedefinePoint(string device, string point);

        /// <summary>
        /// 获得指定设备下的重定义信号
        /// </summary>
        List<D_RedefinePoint> GetRedefinePointsInDevice(string id);

        /// <summary>
        /// 获得所有的重定义信号
        /// </summary>
        List<D_RedefinePoint> GetRedefinePoints();
    }
}
