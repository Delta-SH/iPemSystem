using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 员工信息表
    /// </summary>
    public partial interface IU_EmployeeRepository {
        /// <summary>
        /// 获得指定编号的员工
        /// </summary>
        U_Employee GetEmployeeById(string id);

        /// <summary>
        /// 获得指定代码的员工
        /// </summary>
        U_Employee GetEmployeeByCode(string id);

        /// <summary>
        /// 获得指定部门的员工
        /// </summary>
        List<U_Employee> GetEmployeesByDept(string id);

        /// <summary>
        /// 获得所有的员工
        /// </summary>
        List<U_Employee> GetEmployees();
    }
}
