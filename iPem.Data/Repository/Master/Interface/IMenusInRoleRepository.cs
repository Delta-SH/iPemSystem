using iPem.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Master {
    /// <summary>
    /// Menus in role repository interface
    /// </summary>
    public partial interface IMenusInRoleRepository {
        MenusInRole GetEntity(Guid role);

        void Insert(MenusInRole entity);

        void Insert(IList<MenusInRole> entities);

        void Delete(Guid role);

        void Delete(IList<Guid> roles);
    }
}
