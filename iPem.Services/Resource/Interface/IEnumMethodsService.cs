using iPem.Core;
using iPem.Core.Domain.Resource;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial interface IEnumMethodsService {

        EnumMethods GetEnumMethods(int id);

        IPagedList<EnumMethods> GetEnumMethods(EnmMethodType type, string comment, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<EnumMethods> GetAllEnumMethods(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
