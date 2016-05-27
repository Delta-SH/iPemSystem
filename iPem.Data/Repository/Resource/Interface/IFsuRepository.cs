using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// Fsu repository interface
    /// </summary>
    public partial interface IFsuRepository {

        Fsu GetEntity(string id);

        List<Fsu> GetEntities();

    }
}
