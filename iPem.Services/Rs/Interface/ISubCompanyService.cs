using iPem.Core;
using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial interface ISubCompanyService {
        SubCompany GetSubCompany(string id);

        IPagedList<SubCompany> GetAllSubCompanies(int pageIndex = 0, int pageSize = int.MaxValue);

        List<SubCompany> GetAllSubCompaniesAsList();
    }
}
