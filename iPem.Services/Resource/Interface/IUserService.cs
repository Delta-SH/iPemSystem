using iPem.Core;
using iPem.Core.Domain.Resource;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial interface IUserService {

        User GetUser(string id);

        IPagedList<User> GetAllUsers(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}
