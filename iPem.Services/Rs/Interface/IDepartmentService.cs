using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IDepartmentService {
        C_Department GetDepartment(string id);

        C_Department GetDepartmentByCode(string code);

        IPagedList<C_Department> GetAllDepartments(int pageIndex = 0, int pageSize = int.MaxValue);

        List<C_Department> GetAllDepartmentsAsList();
    }
}
