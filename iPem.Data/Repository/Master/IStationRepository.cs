using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Station repository interface
    /// </summary>
    public partial interface IStationRepository {
        List<Station> GetEntities();
    }
}
