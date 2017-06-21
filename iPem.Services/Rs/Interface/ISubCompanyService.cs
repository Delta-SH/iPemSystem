using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    /// <summary>
    /// 代维公司API
    /// </summary>
    public partial interface ISubCompanyService {
        /// <summary>
        /// 获得指定的代维公司
        /// </summary>
        C_SubCompany GetCompany(string id);

        /// <summary>
        /// 获得所有的代维公司
        /// </summary>
        List<C_SubCompany> GetCompanies();

        /// <summary>
        /// 获得所有的代维公司(分页)
        /// </summary>
        IPagedList<C_SubCompany> GetPagedCompanies(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
