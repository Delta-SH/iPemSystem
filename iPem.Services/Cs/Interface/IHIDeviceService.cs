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
        /// 获得所有的设备
        /// </summary>
        List<H_IDevice> GetDevices(DateTime date);

        /// <summary>
        /// 获得所有的设备(分页)
        /// </summary>
        IPagedList<H_IDevice> GetPagedDevices(DateTime date, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
