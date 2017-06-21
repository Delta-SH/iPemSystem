using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 信号重定义API
    /// </summary>
    public partial interface IRedefinePointService {
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

        /// <summary>
        /// 获得所有的机房类型
        /// </summary>
        IPagedList<D_RedefinePoint> GetPagedRedefinePoints(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
