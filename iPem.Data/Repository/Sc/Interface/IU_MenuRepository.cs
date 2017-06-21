using iPem.Core.Domain.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Data.Repository.Sc {
    /// <summary>
    /// 系统菜单表
    /// </summary>
    public partial interface IU_MenuRepository {
        /// <summary>
        /// 获得指定的系统菜单
        /// </summary>
        U_Menu GetMenu(int id);

        /// <summary>
        /// 获得指定角色的系统菜单
        /// </summary>
        List<U_Menu> GetMenusInRole(Guid id);

        /// <summary>
        /// 获得全部的系统菜单
        /// </summary>
        List<U_Menu> GetMenus();

        /// <summary>
        /// 新增系统菜单
        /// </summary>
        void Insert(IList<U_Menu> entities);

        /// <summary>
        /// 更新系统菜单
        /// </summary>
        void Update(IList<U_Menu> entities);

        /// <summary>
        /// 删除系统菜单
        /// </summary>
        void Delete(IList<U_Menu> entities);
    }
}