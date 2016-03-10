using System;
using iPem.Core;
using iPem.Core.Domain.Master;

namespace iPem.Services.Master {
    /// <summary>
    /// MenuService interface
    /// </summary>
    public partial interface IMenuService {
        Menu GetMenu(int id);

        IPagedList<Menu> GetAllMenus(int pageIndex = 0, int pageSize = int.MaxValue);

        IPagedList<Menu> GetMenus(Guid roleId, int pageIndex = 0, int pageSize = int.MaxValue);

        void InsertMenu(Menu menu);

        void UpdateMenu(Menu menu);

        void DeleteMenu(Menu menu);
    }
}
