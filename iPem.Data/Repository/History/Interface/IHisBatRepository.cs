using iPem.Core.Domain.History;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.History {
    public partial interface IHisBatRepository {

        List<HisBat> GetEntities(string device, DateTime start, DateTime end);

        List<HisBat> GetEntities(string device, string point, DateTime start, DateTime end);

        List<HisBat> GetEntities(DateTime start, DateTime end);

    }
}