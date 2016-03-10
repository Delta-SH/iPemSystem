using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    public partial interface ISubCompanyRepository {

        SubCompany GetEntity(string id);

        List<SubCompany> GetEntities();

    }
}
