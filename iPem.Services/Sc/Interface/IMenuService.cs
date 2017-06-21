using System;
using iPem.Core;
using iPem.Core.Domain.Sc;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    /// <summary>
    /// 系统菜单API
    /// </summary>
    public partial interface IMenuService {
        /// <summary>
        /// 获得指定的系统菜单
        /// </summary>
        U_Menu GetMenu(int id);

        /// <summary>
        /// 获得全部的系统菜单
        /// </summary>
        List<U_Menu> GetMenus();

        /// <summary>
        /// 获得指定角色的系统菜单
        /// </summary>
        List<U_Menu> GetMenusInRole(Guid id);

        /// <summary>
        /// 新增系统菜单
        /// </summary>
        void Add(params U_Menu[] menus);

        /// <summary>
        /// 更新系统菜单
        /// </summary>
        void Update(params U_Menu[] menus);

        /// <summary>
        /// 删除系统菜单
        /// </summary>
        void Remove(params U_Menu[] menus);
    }
}
