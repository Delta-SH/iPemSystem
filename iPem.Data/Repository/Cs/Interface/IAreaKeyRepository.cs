using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    /// <summary>
    /// Area repository interface
    /// </summary>
    public partial interface IAreaKeyRepository {
        List<AreaKey> GetEntities();
    }
}
