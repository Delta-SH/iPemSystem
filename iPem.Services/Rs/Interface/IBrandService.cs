using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IBrandService {
        C_Brand GetBrand(string id);

        IPagedList<C_Brand> GetAllBrands(int pageIndex = 0, int pageSize = int.MaxValue);

        List<C_Brand> GetAllBrandsAsList();
    }
}
