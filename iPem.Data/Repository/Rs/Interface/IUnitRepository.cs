using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface IUnitRepository {

        Unit GetEntity(string id);

        List<Unit> GetEntities();

    }
}
