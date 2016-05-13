using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// Logic Type repository interface
    /// </summary>
    public partial interface ILogicTypeRepository {

        LogicType GetEntity(string id);

        List<LogicType> GetEntities();

    }
}
