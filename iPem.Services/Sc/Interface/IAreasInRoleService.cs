using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IAreasInRoleService {
        IPagedList<string> GetKeys(Guid role, int pageIndex = 0, int pageSize = int.MaxValue);

        List<string> GetKeysAsList(Guid role);

        void Add(Guid role, List<string> keys);

        void Remove(Guid role);
    }
}
