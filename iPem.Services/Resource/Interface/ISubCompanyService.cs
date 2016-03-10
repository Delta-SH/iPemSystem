using iPem.Core;
using iPem.Core.Domain.Resource;
using System;

namespace iPem.Services.Resource {
    public partial interface ISubCompanyService {

        SubCompany GetSubCompany(string id);

        IPagedList<SubCompany> GetAllSubCompanies(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
