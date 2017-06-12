using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    /// <summary>
    /// 代维公司表
    /// </summary>
    public partial interface IC_SubCompanyRepository {
        /// <summary>
        /// 获得指定的代维公司
        /// </summary>
        C_SubCompany GetCompany(string id);

        /// <summary>
        /// 获得所有的代维公司
        /// </summary>
        List<C_SubCompany> GetCompanies();
    }
}
