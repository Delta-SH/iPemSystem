using System;
using iPem.Core;
using iPem.Core.Domain.Sc;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IMenuService {
        Menu GetMenu(int id);

        IPagedList<Menu> GetAllMenus(int pageIndex = 0, int pageSize = int.MaxValue);

        List<Menu> GetAllMenusAsList();

        IPagedList<Menu> GetMenus(Guid roleId, int pageIndex = 0, int pageSize = int.MaxValue);

        List<Menu> GetMenusAsList(Guid role);

        void InsertMenu(Menu menu);

        void UpdateMenu(Menu menu);

        void DeleteMenu(Menu menu);
    }
}
