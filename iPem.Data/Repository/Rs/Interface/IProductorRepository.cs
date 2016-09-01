using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface IProductorRepository {

        Productor GetEntity(string id);

        List<Productor> GetEntities();

    }
}
