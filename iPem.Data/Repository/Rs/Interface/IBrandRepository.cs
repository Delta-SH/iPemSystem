using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface IBrandRepository {

        Brand GetEntity(string id);

        List<Brand> GetEntities();

    }
}
