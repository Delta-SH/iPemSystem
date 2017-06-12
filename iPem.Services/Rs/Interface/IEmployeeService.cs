using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface IEmployeeService {
        U_Employee GetEmpolyee(string id);

        U_Employee GetEmpolyeeByCode(string code);

        IPagedList<U_Employee> GetEmployeesByDept(string dept, int pageIndex = 0, int pageSize = int.MaxValue);

        List<U_Employee> GetEmployeesByDeptAsList(string dept);

        IPagedList<U_Employee> GetAllEmployees(int pageIndex = 0, int pageSize = int.MaxValue);

        List<U_Employee> GetAllEmployeesAsList();
    }
}