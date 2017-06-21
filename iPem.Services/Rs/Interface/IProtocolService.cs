using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 设备模版API
    /// </summary>
    public partial interface IProtocolService {
        /// <summary>
        /// 获得所有的设备模版
        /// </summary>
        List<P_Protocol> GetProtocols();

        /// <summary>
        /// 获得所有的设备模版（分页）
        /// </summary>
        IPagedList<P_Protocol> GetPagedProtocols(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
