using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface ILogicTypeService {
        LogicType GetLogicType(string id);

        SubLogicType GetSubLogicType(string id);

        IPagedList<LogicType> GetAllLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue);

        List<LogicType> GetAllLogicTypesAsList();

        IPagedList<SubLogicType> GetAllSubLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue);

        List<SubLogicType> GetAllSubLogicTypesAsList();

        IPagedList<SubLogicType> GetSubLogicTypes(string parent, int pageIndex = 0, int pageSize = int.MaxValue);

        List<SubLogicType> GetSubLogicTypesAsList(string parent);
    }
}
