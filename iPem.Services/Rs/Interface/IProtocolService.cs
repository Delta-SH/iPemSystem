using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IProtocolService {
        IPagedList<Protocol> GetAllProtocols(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Protocol> GetAllProtocolsAsList();
    }
}
