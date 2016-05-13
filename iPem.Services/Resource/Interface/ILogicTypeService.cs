using iPem.Core;
using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial interface ILogicTypeService {

        LogicType GetLogicType(string id);

        IPagedList<LogicType> GetAllLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
