using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 摄像机信息API
    /// </summary>
    public partial interface ICameraService {
        /// <summary>
        /// 获得摄像机信息
        /// </summary>
        V_Camera GetCamera(string id);

        /// <summary>
        /// 获得所有的摄像机信息
        /// </summary>
        List<V_Camera> GetCameras();

        /// <summary>
        /// 获得所有的摄像机信息
        /// </summary>
        IPagedList<V_Camera> GetPagedCameras(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
