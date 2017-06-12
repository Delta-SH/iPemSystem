using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 设备模版表
    /// </summary>
    public partial interface IP_ProtocolRepository {
        /// <summary>
        /// 获得所有的设备模版
        /// </summary>
        /// <returns></returns>
        List<P_Protocol> GetProtocols();
    }
}
