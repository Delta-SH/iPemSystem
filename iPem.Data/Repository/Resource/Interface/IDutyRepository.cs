using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// Duty repository interface
    /// </summary>
    public partial interface IDutyRepository {

        Duty GetEntity(string id);

        List<Duty> GetEntities();

    }
}
