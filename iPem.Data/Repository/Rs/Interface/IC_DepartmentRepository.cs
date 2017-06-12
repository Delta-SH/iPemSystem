using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 部门信息表
    /// </summary>
    public partial interface IC_DepartmentRepository {
        /// <summary>
        /// 获得指定编号的部门信息
        /// </summary>
        C_Department GetDepartmentById(string id);

        /// <summary>
        /// 获得指定代码的部门信息
        /// </summary>
        C_Department GetDepartmentByCode(string code);

        /// <summary>
        /// 获得所有部门信息
        /// </summary>
        List<C_Department> GetDepartments();
    }
}
