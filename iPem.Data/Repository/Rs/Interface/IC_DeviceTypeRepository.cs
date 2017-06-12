using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 设备类型/设备子类型信息表
    /// </summary>
    public partial interface IC_DeviceTypeRepository {
        /// <summary>
        /// 获得指定的设备类型信息
        /// </summary>
        C_DeviceType GetDeviceType(string id);

        /// <summary>
        /// 获得指定的设备子类型信息
        /// </summary>
        C_SubDeviceType GetSubDeviceType(string id);

        /// <summary>
        /// 获得所有的设备类型信息
        /// </summary>
        List<C_DeviceType> GetDeviceTypes();

        /// <summary>
        /// 获得所有的设备子类型信息
        /// </summary>
        List<C_SubDeviceType> GetSubDeviceTypes();
    }
}
