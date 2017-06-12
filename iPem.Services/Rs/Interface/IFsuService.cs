using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IFsuService {
        D_Fsu GetFsu(string id);

        IPagedList<D_Fsu> GetAllFsus(int pageIndex = 0, int pageSize = int.MaxValue);

        List<D_Fsu> GetAllFsusAsList();

        D_ExtFsu GetFsuExt(string id);

        IPagedList<D_ExtFsu> GetAllExtends(int pageIndex = 0, int pageSize = int.MaxValue);

        List<D_ExtFsu> GetAllExtendsAsList();
    }
}
