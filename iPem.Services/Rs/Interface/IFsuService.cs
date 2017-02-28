using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IFsuService {
        Fsu GetFsu(string id);

        IPagedList<Fsu> GetAllFsus(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Fsu> GetAllFsusAsList();

        IPagedList<FsuExt> GetAllExtends(int pageIndex = 0, int pageSize = int.MaxValue);

        List<FsuExt> GetAllExtendsAsList();
    }
}
