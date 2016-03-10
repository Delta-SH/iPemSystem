using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    public partial interface ISupplierRepository {

        Supplier GetEntity(string id);

        List<Supplier> GetEntities();

    }
}
