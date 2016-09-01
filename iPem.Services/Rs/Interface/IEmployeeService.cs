using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IEmployeeService {
        Employee GetEmpolyee(string id);

        Employee GetEmpolyeeByCode(string code);

        IPagedList<Employee> GetAllEmployees(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Employee> GetAllEmployeesAsList();

        IPagedList<Employee> GetEmployees(string dept, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Employee> GetEmployeesAsList(string dept);
    }
}