using iPem.Core;
using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    /// <summary>
    /// EmployeeService interface
    /// </summary>
    public partial interface IEmployeeService {
        Employee GetEmpolyee(string id);

        Employee GetEmpolyeeByNo(string id);

        IPagedList<Employee> GetAllEmployees(int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Employee> GetDeptEmployees(string id, int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
