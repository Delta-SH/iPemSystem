using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface IEmployeeRepository {
        Employee GetEntity(string id);

        Employee GetEntityByCode(string code);

        List<Employee> GetEntities(string dept);

        List<Employee> GetEntities();
    }
}
