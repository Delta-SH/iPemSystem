using iPem.Core;
using iPem.Core.Domain.Resource;
using System;

namespace iPem.Services.Resource {
    /// <summary>
    /// DepartmentService interface
    /// </summary>
    public partial interface IDepartmentService {
        Department GetDepartment(string id);

        Department GetDepartmentByCode(string code);

        IPagedList<Department> GetAllDepartments(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
