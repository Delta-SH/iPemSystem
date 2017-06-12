using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IDeviceTypeService {
        C_DeviceType GetDeviceType(string id);

        C_SubDeviceType GetSubDeviceType(string id);

        IPagedList<C_DeviceType> GetAllDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue);

        List<C_DeviceType> GetAllDeviceTypesAsList();

        IPagedList<C_SubDeviceType> GetAllSubDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue);

        List<C_SubDeviceType> GetAllSubDeviceTypesAsList();
    }
}
