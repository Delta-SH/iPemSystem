using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IDepartmentService {
        Department GetDepartment(string id);

        Department GetDepartmentByCode(string code);

        IPagedList<Department> GetAllDepartments(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Department> GetAllDepartmentsAsList();
    }
}
