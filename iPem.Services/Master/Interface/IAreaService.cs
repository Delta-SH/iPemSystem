using iPem.Core;
using iPem.Core.Domain.Master;
using System;

namespace iPem.Services.Master {
    /// <summary>
    /// AreaService interface
    /// </summary>
    public partial interface IAreaService {
        IPagedList<Area> GetAreasInRole(Guid role, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Area> GetAllAreas(int pageIndex = 0, int pageSize = int.MaxValue);

        void AddArea(Area area);

        void RemoveArea(Area area);
    }
}
