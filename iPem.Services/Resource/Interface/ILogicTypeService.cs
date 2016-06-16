using iPem.Core;
using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial interface ILogicTypeService {

        LogicType GetLogicType(string id);

        SubLogicType GetSubLogicType(string id);

        IPagedList<LogicType> GetAllLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<SubLogicType> GetSubLogicTypes(string parent, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<SubLogicType> GetAllSubLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
