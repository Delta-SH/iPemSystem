using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IExtendAlmService {

        IPagedList<ExtAlm> GetAllExtAlms(int pageIndex = 0, int pageSize = int.MaxValue);

        List<ExtAlm> GetAllExtAlmsAsList();

        void Update(List<ExtAlm> entities);

        IPagedList<ExtAlm> GetHisExtAlms(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<ExtAlm> GetHisExtAlmsAsList(DateTime start, DateTime end);

    }
}
