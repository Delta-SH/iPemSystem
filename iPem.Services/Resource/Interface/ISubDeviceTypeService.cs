using iPem.Core;
using iPem.Core.Domain.Resource;
using System;

namespace iPem.Services.Resource {
    public partial interface ISubDeviceTypeService {

        SubDeviceType GetSubDeviceType(string id);

        IPagedList<SubDeviceType> GetAllSubDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}