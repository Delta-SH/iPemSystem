using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 设备信息表
    /// </summary>
    public partial interface ID_DeviceRepository {
        /// <summary>
        /// 获得指定的设备
        /// </summary>
        D_Device GetDevice(string id);

        /// <summary>
        /// 获得指定站点下的设备
        /// </summary>
        List<D_Device> GetDevicesInStation(string id);

        /// <summary>
        /// 获得指定机房下的设备
        /// </summary>
        List<D_Device> GetDevicesInRoom(string id);

        /// <summary>
        /// 获得指定FSU下的设备
        /// </summary>
        List<D_Device> GetDevicesInFsu(string id);

        /// <summary>
        /// 获得所有的设备
        /// </summary>
        /// <returns></returns>
        List<D_Device> GetDevices();
    }
}
