using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// Logic Type repository interface
    /// </summary>
    public partial interface ILogicTypeRepository {

        LogicType GetEntity(string id);

        SubLogicType GetSubEntity(string id);

        List<LogicType> GetEntities();

        List<SubLogicType> GetSubEntities(string logic);

        List<SubLogicType> GetSubEntities();

    }
}
