using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface ISubCompanyService {
        C_SubCompany GetSubCompany(string id);

        IPagedList<C_SubCompany> GetAllSubCompanies(int pageIndex = 0, int pageSize = int.MaxValue);

        List<C_SubCompany> GetAllSubCompaniesAsList();
    }
}
