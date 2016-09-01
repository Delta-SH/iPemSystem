using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IDutyService {
        Duty GetDuty(string id);

        IPagedList<Duty> GetAllDuties(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Duty> GetAllDutiesAsList();
    }
}
