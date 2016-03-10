using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    public partial interface IAreasInRoleRepository {

        AreasInRole GetEntities(Guid role);

        void Insert(AreasInRole entity);

        void Insert(List<AreasInRole> entities);

        void Delete(Guid role);

        void Delete(List<Guid> roles);

    }
}