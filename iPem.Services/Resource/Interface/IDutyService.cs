using iPem.Core;
using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial interface IDutyService {

        Duty GetDuty(string id);

        IPagedList<Duty> GetAllDuties(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
