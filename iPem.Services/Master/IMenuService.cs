using System;
using iPem.Core;
using iPem.Core.Domain.Master;

namespace iPem.Services.Master {
    /// <summary>
    /// MenuService interface
    /// </summary>
    public partial interface IMenuService {
        Menu GetMenu(int id);

        IPagedList<Menu> GetAllMenus(Guid roleId, int pageIndex, int pageSize);

        void InsertMenu(Menu menu);

        void UpdateMenu(Menu menu);

        void DeleteMenu(Menu menu);
    }
}
