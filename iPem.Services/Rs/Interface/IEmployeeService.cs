using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 员工信息API
    /// </summary>
    public partial interface IEmployeeService {
        /// <summary>
        /// 获得指定编号的员工
        /// </summary>
        U_Employee GetEmployeeById(string id);

        /// <summary>
        /// 获得指定代码的员工
        /// </summary>
        U_Employee GetEmployeeByCode(string code);

        /// <summary>
        /// 获得指定部门的员工
        /// </summary>
        List<U_Employee> GetEmployeesByDept(string dept);

        /// <summary>
        /// 获得所有的员工
        /// </summary>
        List<U_Employee> GetEmployees();

        /// <summary>
        /// 获得所有的员工(分页)
        /// </summary>
        IPagedList<U_Employee> GetPagedEmployees(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}