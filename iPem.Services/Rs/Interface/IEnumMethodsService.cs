using iPem.Core;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IEnumMethodsService {
        C_EnumMethod GetValue(int id);

        IPagedList<C_EnumMethod> GetAllValues(int pageIndex = 0, int pageSize = int.MaxValue);

        List<C_EnumMethod> GetAllValuesAsList();

        IPagedList<C_EnumMethod> GetValues(EnmMethodType type, string comment, int pageIndex = 0, int pageSize = int.MaxValue);

        List<C_EnumMethod> GetValuesAsList(EnmMethodType type, string comment);
    }
}
