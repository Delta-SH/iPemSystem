using iPem.Core;
using iPem.Core.Domain.Resource;
using System;

namespace iPem.Services.Resource {
    public partial interface IProductorService {

        Productor GetProductor(string id);

        IPagedList<Productor> GetAllProductors(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
