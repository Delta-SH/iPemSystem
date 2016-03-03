using iPem.Core.Domain.Master;
using System;

namespace iPem.Services.Master {
    /// <summary>
    /// AreasInRole Service interface
    /// </summary>
    public partial interface IAreasInRoleService {

        AreasInRole GetAreasInRole(Guid id);

        void AddAreasInRole(AreasInRole areas);

        void DeleteAreasInRole(Guid id);

    }
}
