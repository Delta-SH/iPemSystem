using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface IStationRepository {
        Station GetEntity(string id);

        List<Station> GetEntities(string parent);

        List<Station> GetEntities();
    }
}
