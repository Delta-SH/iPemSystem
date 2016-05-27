using iPem.Core;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    public partial interface IPointsInProtcolService {

        IPagedList<IdValuePair<string, string>> GetAllPointsInProtcol(int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<IdValuePair<string, string>> GetRelation(int pageIndex = 0, int pageSize = int.MaxValue);
        
    }
}
