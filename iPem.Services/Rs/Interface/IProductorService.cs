using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IProductorService {
        C_Productor GetProductor(string id);

        IPagedList<C_Productor> GetAllProductors(int pageIndex = 0, int pageSize = int.MaxValue);

        List<C_Productor> GetAllProductorsAsList();
    }
}
