using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Area repository interface
    /// </summary>
    public partial interface IAreaRepository {
        IList<Area> GetEntities(Guid role);

        IList<Area> GetEntities();

        void Insert(Area entity);

        void Insert(IList<Area> entities);

        void Delete(Area entity);

        void Delete(IList<Area> entities);
    }
}
