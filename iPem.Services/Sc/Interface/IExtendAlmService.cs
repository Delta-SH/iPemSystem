using iPem.Core;
using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IExtendAlmService {

        IPagedList<ExtAlarm> GetAllExtAlms(int pageIndex = 0, int pageSize = int.MaxValue);

        List<ExtAlarm> GetAllExtAlmsAsList();

        void Update(List<ExtAlarm> entities);

        IPagedList<ExtAlarm> GetHisExtAlms(DateTime start, DateTime end, int pageIndex = 0, int pageSize = int.MaxValue);

        List<ExtAlarm> GetHisExtAlmsAsList(DateTime start, DateTime end);

    }
}
