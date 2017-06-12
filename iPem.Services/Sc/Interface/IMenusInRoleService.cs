using iPem.Core;
using iPem.Core.Domain.Sc;
using System;

namespace iPem.Services.Sc {
    public partial interface IMenusInRoleService {
        U_EntitiesInRole GetMenusInRole(Guid id);

        void AddMenusInRole(U_EntitiesInRole menus);

        void DeleteMenusInRole(Guid id);
    }
}
