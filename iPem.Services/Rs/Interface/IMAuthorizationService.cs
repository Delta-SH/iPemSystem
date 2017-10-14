using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 门禁设备授权API
    /// </summary>
    public partial interface IMAuthorizationService {
        /// <summary>
        /// 获得所有的授权设备
        /// </summary>
        List<M_Authorization> GetAuthorizations();

        /// <summary>
        /// 获得指定人员类型的授权设备
        /// </summary>
        List<M_Authorization> GetAuthorizationsInType(EnmEmpType type);

        /// <summary>
        /// 获得指定卡号的授权设备
        /// </summary>
        List<M_Authorization> GetAuthorizationsInCard(string card);
    }
}
