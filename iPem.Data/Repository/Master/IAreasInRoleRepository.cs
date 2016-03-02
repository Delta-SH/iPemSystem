using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    public partial interface IAreasInRoleRepository {

        AreasInRole GetEntities(Guid role);

        void Insert(AreasInRole entity);

        void Insert(IList<AreasInRole> entities);

        void Delete(Guid role);

        void Delete(IList<Guid> roles);

    }
}