using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface ISupplierRepository {

        Supplier GetEntity(string id);

        List<Supplier> GetEntities();

    }
}
