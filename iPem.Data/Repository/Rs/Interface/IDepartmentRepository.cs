using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface IDepartmentRepository {
        Department GetEntity(string id);

        Department GetEntityByCode(string code);

        List<Department> GetEntities();
    }
}
