using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IEnumMethodsService {
        EnumMethods GetValue(int id);

        IPagedList<EnumMethods> GetAllValues(int pageIndex = 0, int pageSize = int.MaxValue);

        List<EnumMethods> GetAllValuesAsList();

        IPagedList<EnumMethods> GetValues(EnmMethodType type, string comment, int pageIndex = 0, int pageSize = int.MaxValue);

        List<EnumMethods> GetValuesAsList(EnmMethodType type, string comment);
    }
}
