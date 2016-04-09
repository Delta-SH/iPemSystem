using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Resource {
    public partial interface IUserRepository {

        User GetEntity(string id);

        List<User> GetEntities();

    }
}
