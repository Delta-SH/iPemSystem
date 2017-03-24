using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    public partial interface IHisLoadRateRepository {
        List<HisLoadRate> GetEntities(DateTime start, DateTime end);

        List<HisLoadRate> GetMaxInDevice(DateTime start, DateTime end, double max);
    }
}
