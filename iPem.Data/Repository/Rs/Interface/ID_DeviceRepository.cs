﻿using iPem.Core.Domain.Rs;
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

        /// <summary>
        /// 获得包含指定信号的设备集合
        /// </summary>
        /// <param name="points">指定的信号</param>
        /// <returns>包含指定信号的设备集合</returns>
        HashSet<string> GetDeviceKeysWithPoints(string[] points);
    }
}
