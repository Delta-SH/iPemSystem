using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IDeviceTypeService {
        DeviceType GetDeviceType(string id);

        SubDeviceType GetSubDeviceType(string id);

        IPagedList<DeviceType> GetAllDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue);

        List<DeviceType> GetAllDeviceTypesAsList();

        IPagedList<SubDeviceType> GetAllSubDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue);

        List<SubDeviceType> GetAllSubDeviceTypesAsList();
    }
}
