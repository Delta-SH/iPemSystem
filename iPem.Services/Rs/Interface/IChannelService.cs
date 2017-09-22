using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 摄像机通道信息API
    /// </summary>
    public partial interface IChannelService {
        /// <summary>
        /// 获得指定摄像机的通道信息
        /// </summary>
        List<V_Channel> GetChannels(string camera);

        /// <summary>
        /// 获得所有摄像机的通道信息
        /// </summary>
        List<V_Channel> GetAllChannels();

        /// <summary>
        /// 获得所有摄像机的通道信息（分页）
        /// </summary>
        IPagedList<V_Channel> GetPagedCameras(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
