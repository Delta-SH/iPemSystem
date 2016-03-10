using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    public partial interface IUnitRepository {

        Unit GetEntity(string id);

        List<Unit> GetEntities();

    }
}
