using iPem.Core.Domain.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Rs {
    public partial interface ISubCompanyRepository {

        SubCompany GetEntity(string id);

        List<SubCompany> GetEntities();

    }
}
