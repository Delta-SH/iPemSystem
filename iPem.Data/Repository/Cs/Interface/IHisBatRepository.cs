using iPem.Core.Domain.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    public partial interface IHisBatRepository {

        List<HisBat> GetEntities(string device, DateTime start, DateTime end);

        List<HisBat> GetEntities(string device, string point, DateTime start, DateTime end);

        List<HisBat> GetEntities(DateTime start, DateTime end);

        List<HisBat> GetProcedures(string device, DateTime start, DateTime end);

        List<HisBat> GetProcedures(string device, string point, DateTime start, DateTime end);

        List<HisBat> GetProcedures(DateTime start, DateTime end);
    }
}