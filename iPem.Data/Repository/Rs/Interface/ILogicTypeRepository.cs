using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface ILogicTypeRepository {

        LogicType GetEntity(string id);

        SubLogicType GetSubEntity(string id);

        List<LogicType> GetEntities();

        List<SubLogicType> GetSubEntities(string parent);

        List<SubLogicType> GetSubEntities();

    }
}
