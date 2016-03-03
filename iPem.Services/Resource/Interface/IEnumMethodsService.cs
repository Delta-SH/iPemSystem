using iPem.Core;
using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial interface IEnumMethodsService {

        EnumMethods GetEnumMethods(string id);

        IPagedList<EnumMethods> GetAllEnumMethods(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
