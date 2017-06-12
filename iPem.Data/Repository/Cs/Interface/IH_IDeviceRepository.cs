using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// 资管接口设备表
    /// </summary>
    public partial interface IH_IDeviceRepository {
        /// <summary>
        /// 获得指定设备类型所包含的设备
        /// </summary>
        List<H_IDevice> GetDevicesInType(string type);

        /// <summary>
        /// 获得指定站点下所包含的设备
        /// </summary>
        /// <param name="parent">站点编号</param>
        List<H_IDevice> GetDevicesInParent(string parent);

        /// <summary>
        /// 获得所有的设备
        /// </summary>
        List<H_IDevice> GetDevices();
    }
}
