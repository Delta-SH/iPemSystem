using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 摄像机信息表
    /// </summary>
    public partial interface IV_CameraRepository {
        /// <summary>
        /// 获得指定的摄像机信息
        /// </summary>
        V_Camera GetEntity(string id);

        /// <summary>
        /// 获得所有的摄像机信息
        /// </summary>
        List<V_Camera> GetEntities();
    }
}
