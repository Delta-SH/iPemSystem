using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface IStationTypeRepository {

        StationType GetEntity(string id);

        List<StationType> GetEntities();

    }
}