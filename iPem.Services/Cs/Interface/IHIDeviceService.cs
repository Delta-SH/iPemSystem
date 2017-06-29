using iPem.Core;
using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    /// <summary>
    /// 资管接口设备API
    /// </summary>
    public partial interface IHIDeviceService {
        /// <summary>
        /// 获得指定设备类型所包含的设备
        /// </summary>
        List<H_IDevice> GetDevicesInTypeId(string type);

        /// <summary>
        /// 获得指定设备类型所包含的设备
        /// </summary>
        List<H_IDevice> GetDevicesInTypeName(string type);

        /// <summary>
        /// 获得指定站点下所包含的设备
        /// </summary>
        /// <param name="parent">站点编号</param>
        List<H_IDevice> GetDevicesInParent(string parent);

        /// <summary>
        /// 获得所有的设备
        /// </summary>
        List<H_IDevice> GetDevices();

        /// <summary>
        /// 获得所有的设备(分页)
        /// </summary>
        IPagedList<H_IDevice> GetPagedDevices(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
