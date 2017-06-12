using System;
using iPem.Core;
using iPem.Core.Domain.Sc;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial interface IMenuService {
        U_Menu GetMenu(int id);

        IPagedList<U_Menu> GetAllMenus(int pageIndex = 0, int pageSize = int.MaxValue);

        List<U_Menu> GetAllMenusAsList();

        IPagedList<U_Menu> GetMenus(Guid roleId, int pageIndex = 0, int pageSize = int.MaxValue);

        List<U_Menu> GetMenusAsList(Guid role);

        void InsertMenu(U_Menu menu);

        void UpdateMenu(U_Menu menu);

        void DeleteMenu(U_Menu menu);
    }
}
