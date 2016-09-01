using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IExtendAlmService {

        IPagedList<ExtAlm> GetAllExtAlms(int pageIndex = 0, int pageSize = int.MaxValue);

        List<ExtAlm> GetAllExtAlmsAsList();

        void Update(List<ExtAlm> entities);

    }
}
