using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IDutyService {
        C_Duty GetDuty(string id);

        IPagedList<C_Duty> GetAllDuties(int pageIndex = 0, int pageSize = int.MaxValue);

        List<C_Duty> GetAllDutiesAsList();
    }
}
