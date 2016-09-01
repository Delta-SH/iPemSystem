using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    public partial interface IMenusInRoleRepository {
        MenusInRole GetEntity(Guid role);

        void Insert(MenusInRole entity);

        void Insert(List<MenusInRole> entities);

        void Delete(Guid role);

        void Delete(List<Guid> roles);
    }
}
