using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface IEnumMethodsRepository {

        EnumMethods GetEntity(int id);

        List<EnumMethods> GetEntities(EnmMethodType type, string comment);

        List<EnumMethods> GetEntities();

    }
}
