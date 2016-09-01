using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IAreasInRoleRepository {

        List<string> GetEntities(Guid role);

        void Insert(Guid role, List<string> keys);

        void Delete(Guid role);

        void Delete(List<Guid> roles);

    }
}