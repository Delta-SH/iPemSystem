using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IEmployeeService {
        Employee GetEmpolyee(string id);

        Employee GetEmpolyeeByCode(string code);

        IPagedList<Employee> GetEmployeesByDept(string dept, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Employee> GetEmployeesByDeptAsList(string dept);

        IPagedList<Employee> GetAllEmployees(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Employee> GetAllEmployeesAsList();
    }
}