using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 门禁设备授权表
    /// </summary>
    public partial interface IM_AuthorizationRepository {
        /// <summary>
        /// 获得所有的授权设备
        /// </summary>
        List<M_Authorization> GetEntities();

        /// <summary>
        /// 获得指定人员类型的授权设备
        /// </summary>
        List<M_Authorization> GetEntitiesInType(EnmEmpType type);

        /// <summary>
        /// 获得指定卡号的授权设备
        /// </summary>
        List<M_Authorization> GetEntitiesInCard(string card);
    }
}
