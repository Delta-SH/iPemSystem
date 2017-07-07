using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 资管接口设备表
    /// </summary>
    public partial interface IH_IDeviceRepository {
        /// <summary>
        /// 获得所有的设备
        /// </summary>
        List<H_IDevice> GetDevices(DateTime date);
    }
}
