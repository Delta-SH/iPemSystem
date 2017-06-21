using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 设备类型/设备子类型信息API
    /// </summary>
    public partial interface IDeviceTypeService {
        C_DeviceType GetDeviceType(string id);

        C_SubDeviceType GetSubDeviceType(string id);

        List<C_DeviceType> GetDeviceTypes();

        List<C_SubDeviceType> GetSubDeviceTypes();

        IPagedList<C_DeviceType> GetPagedDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<C_SubDeviceType> GetPagedSubDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
