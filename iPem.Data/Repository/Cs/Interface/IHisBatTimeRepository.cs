using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    public partial interface IHisBatTimeRepository {
        List<HisBatTime> GetEntities(DateTime start, DateTime end);

        List<HisBatTime> GetMaxEntities(DateTime start, DateTime end);

        List<HisBatTime> GetMinEntities(DateTime start, DateTime end);
    }
}