using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    public partial interface IHisLoadRateRepository {
        List<HisLoadRate> GetEntities(DateTime start, DateTime end);

        List<HisLoadRate> GetMaxEntities(DateTime start, DateTime end);

        List<HisLoadRate> GetMinEntities(DateTime start, DateTime end);
    }
}
