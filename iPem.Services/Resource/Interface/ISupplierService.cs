using iPem.Core;
using iPem.Core.Domain.Resource;
using System;

namespace iPem.Services.Resource {
    public partial interface ISupplierService {

        Supplier GetSupplier(string id);

        IPagedList<Supplier> GetAllSuppliers(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}