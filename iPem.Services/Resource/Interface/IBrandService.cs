using iPem.Core;
using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial interface IBrandService {

        Brand GetBrand(string id);

        IPagedList<Brand> GetAllBrands(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
