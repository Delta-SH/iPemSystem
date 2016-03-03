using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// EnumMethods repository interface
    /// </summary>
    public partial interface IEnumMethodsRepository {

        EnumMethods GetEntity(string id);

        List<EnumMethods> GetEntities();

    }
}
