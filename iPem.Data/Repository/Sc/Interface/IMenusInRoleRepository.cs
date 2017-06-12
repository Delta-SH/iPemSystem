using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IMenusInRoleRepository {
        U_EntitiesInRole GetEntity(Guid role);

        void Insert(U_EntitiesInRole entity);

        void Insert(List<U_EntitiesInRole> entities);

        void Delete(Guid role);

        void Delete(List<Guid> roles);
    }
}
