using iPem.Core.Domain.Am;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Am {
    public partial interface IAmDeviceRepository {
        List<AmDevice> GetEntities(string type);

        List<AmDevice> GetEntities();
    }
}
