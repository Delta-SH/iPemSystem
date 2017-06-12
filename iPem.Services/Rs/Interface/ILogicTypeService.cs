using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface ILogicTypeService {
        C_LogicType GetLogicType(string id);

        C_SubLogicType GetSubLogicType(string id);

        IPagedList<C_LogicType> GetAllLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue);

        List<C_LogicType> GetAllLogicTypesAsList();

        IPagedList<C_SubLogicType> GetAllSubLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue);

        List<C_SubLogicType> GetAllSubLogicTypesAsList();

        IPagedList<C_SubLogicType> GetSubLogicTypes(string parent, int pageIndex = 0, int pageSize = int.MaxValue);

        List<C_SubLogicType> GetSubLogicTypesAsList(string parent);
    }
}
