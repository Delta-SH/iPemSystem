using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IProductorService {
        Productor GetProductor(string id);

        IPagedList<Productor> GetAllProductors(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Productor> GetAllProductorsAsList();
    }
}
