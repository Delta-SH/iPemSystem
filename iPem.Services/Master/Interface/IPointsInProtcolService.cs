using iPem.Core;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    public partial interface IPointsInProtcolService {

        IPagedList<IdValuePair<int, string>> GetAllPointsInProtcol(int pageIndex = 0, int pageSize = int.MaxValue);
        
    }
}
