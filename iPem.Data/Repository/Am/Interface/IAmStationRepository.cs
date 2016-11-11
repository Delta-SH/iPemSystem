using iPem.Core.Domain.Am;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Am {
    public partial interface IAmStationRepository {
        List<AmStation> GetEntities(string type);

        List<AmStation> GetEntities();
    }
}
