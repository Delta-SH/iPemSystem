using System;
using iPem.Core;
using iPem.Core.Domain.Master;
using iPem.Core.Enum;

namespace iPem.Services.Master {
    /// <summary>
    /// RoleService interface
    /// </summary>
    public partial interface IRoleService {
        Role GetRole(Guid id);

        Role GetRole(string name);

        Role GetRoleByUid(Guid uid);

        IPagedList<Role> GetAllRoles(int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Role> GetAllRoles(Guid id, int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Role> GetAllRoles(string[] names, int pageIndex = 0, int pageSize = int.MaxValue);

        void InsertRole(Role role);

        void UpdateRole(Role role);

        void DeleteRole(Role role);

        EnmLoginResults Validate(Guid uid);
    }
}
