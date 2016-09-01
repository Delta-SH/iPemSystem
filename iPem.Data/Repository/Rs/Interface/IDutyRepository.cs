using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface IDutyRepository {

        Duty GetEntity(string id);

        List<Duty> GetEntities();

    }
}
