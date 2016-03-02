using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// Department repository interface
    /// </summary>
    public partial interface IDepartmentRepository {
        Department GetEntity(string id);

        Department GetEntityByCode(string code);

        IList<Department> GetEntities();
    }
}
