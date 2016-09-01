using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IBrandService {
        Brand GetBrand(string id);

        IPagedList<Brand> GetAllBrands(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Brand> GetAllBrandsAsList();
    }
}
