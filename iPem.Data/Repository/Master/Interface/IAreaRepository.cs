using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Area repository interface
    /// </summary>
    public partial interface IAreaRepository {
        List<Area> GetEntities(Guid role);

        List<Area> GetEntities();

        void Insert(Area entity);

        void Insert(List<Area> entities);

        void Delete(Area entity);

        void Delete(List<Area> entities);
    }
}
