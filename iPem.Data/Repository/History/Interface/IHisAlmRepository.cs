using iPem.Core.Domain.History;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.History {
    public partial interface IHisAlmRepository {

        List<HisAlm> GetEntities(string device, DateTime start, DateTime end);

        List<HisAlm> GetEntities(DateTime start, DateTime end);

    }
}
