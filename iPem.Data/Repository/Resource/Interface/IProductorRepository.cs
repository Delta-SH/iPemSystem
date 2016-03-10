using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    public partial interface IProductorRepository {

        Productor GetEntity(string id);

        List<Productor> GetEntities();

    }
}
