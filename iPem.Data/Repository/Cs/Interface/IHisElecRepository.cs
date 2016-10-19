using iPem.Core.Domain.Cs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Cs {
    public partial interface IHisElecRepository {

        List<HisElec> GetEntities(string id, EnmOrganization type, DateTime start, DateTime end);

        List<HisElec> GetEntities(EnmOrganization type, DateTime start, DateTime end);

        List<HisElec> GetEntities(DateTime start, DateTime end);

    }
}
