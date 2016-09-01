using iPem.Core;
using iPem.Core.Domain.Sc;
using System;

namespace iPem.Services.Sc {
    public partial interface IMenusInRoleService {
        MenusInRole GetMenusInRole(Guid id);

        void AddMenusInRole(MenusInRole menus);

        void DeleteMenusInRole(Guid id);
    }
}
