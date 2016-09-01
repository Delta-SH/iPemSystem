using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IRoleService {
        Role GetRole(Guid id);

        Role GetRole(string name);

        Role GetRoleByUid(Guid uid);

        IPagedList<Role> GetAllRoles(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Role> GetAllRolesAsList();

        IPagedList<Role> GetRoles(Guid id, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Role> GetRolesAsList(Guid id);

        IPagedList<Role> GetRoles(string[] names, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Role> GetRolesAsList(string[] names);

        void Add(Role role);

        void Update(Role role);

        void Remove(Role role);

        EnmLoginResults Validate(Guid uid);
    }
}
