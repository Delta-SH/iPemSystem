using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    /// <summary>
    /// Employees repository interface
    /// </summary>
    public partial interface IEmployeeRepository {
        Employee GetEntity(string id);

        Employee GetEntityByNo(string no);

        List<Employee> GetEntities(string deptId);

        List<Employee> GetEntities();
    }
}
