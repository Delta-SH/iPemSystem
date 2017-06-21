using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 部门信息API
    /// </summary>
    public partial interface IDepartmentService {
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

        /// <summary>
        /// 获得所有部门信息(分页)
        /// </summary>
        IPagedList<C_Department> GetPagedDepartments(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
