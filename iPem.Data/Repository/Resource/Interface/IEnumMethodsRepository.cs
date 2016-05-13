using iPem.Core.Domain.Resource;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// EnumMethods repository interface
    /// </summary>
    public partial interface IEnumMethodsRepository {

        EnumMethods GetEntity(int id);

        List<EnumMethods> GetEntities(EnmMethodType type, string comment);

        List<EnumMethods> GetEntities();

    }
}
