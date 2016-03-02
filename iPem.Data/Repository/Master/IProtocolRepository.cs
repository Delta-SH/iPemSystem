using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Protocol repository interface
    /// </summary>
    public partial interface IProtocolRepository {
        List<Protocol> GetEntities(int deviceType);

        List<Protocol> GetEntities(int deviceType, int subDevType);

        List<Protocol> GetEntities();
    }
}
