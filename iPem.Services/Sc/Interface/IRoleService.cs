using iPem.Core;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IRoleService {
        U_Role GetRole(Guid id);

        U_Role GetRole(string name);

        U_Role GetRoleByUid(Guid uid);

        IPagedList<U_Role> GetAllRoles(int pageIndex = 0, int pageSize = int.MaxValue);

        List<U_Role> GetAllRolesAsList();

        IPagedList<U_Role> GetRoles(Guid id, int pageIndex = 0, int pageSize = int.MaxValue);

        List<U_Role> GetRolesAsList(Guid id);

        IPagedList<U_Role> GetRoles(string[] names, int pageIndex = 0, int pageSize = int.MaxValue);

        List<U_Role> GetRolesAsList(string[] names);

        void Add(U_Role role);

        void Update(U_Role role);

        void Remove(U_Role role);

        EnmLoginResults Validate(Guid uid);
    }
}
