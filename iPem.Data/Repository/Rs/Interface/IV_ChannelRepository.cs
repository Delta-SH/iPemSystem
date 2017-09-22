using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 摄像头信息表
    /// </summary>
    public partial interface IV_ChannelRepository {
        /// <summary>
        /// 获得指定摄像机的通道信息
        /// </summary>
        List<V_Channel> GetEntities(string camera);

        /// <summary>
        /// 获得所有摄像机的通道信息
        /// </summary>
        List<V_Channel> GetEntities();
    }
}
