using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// Brand repository interface
    /// </summary>
    public partial interface IBrandRepository {

        Brand GetEntity(string id);

        List<Brand> GetEntities();

    }
}
