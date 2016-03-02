using iPem.Core;
using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    /// <summary>
    /// IProtocolService interface
    /// </summary>
    public partial interface IProtocolService {
        IPagedList<Protocol> GetProtocolsByDeviceType(int deviceType, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Protocol> GetProtocols(int deviceType, int subDevType, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Protocol> GetAllProtocols(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
