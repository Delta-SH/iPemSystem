using iPem.Core;
using iPem.Core.Domain.Master;
using System;

namespace iPem.Services.Master {
    /// <summary>
    /// MenusInRole Service interface
    /// </summary>
    public partial interface IMenusInRoleService {
        /// <summary>
        /// Gets the menus in role.
        /// </summary>
        /// <param name="id">role id</param>
        /// <returns>menus</returns>
        MenusInRole GetMenusInRole(Guid id);

        /// <summary>
        /// Add the menus in role.
        /// </summary>
        /// <param name="menus">menus in role</param>
        /// <returns>menus</returns>
        void AddMenusInRole(MenusInRole menus);

        /// <summary>
        /// Delete the menus in role.
        /// </summary>
        /// <param name="id">role id</param>
        /// <returns>menus</returns>
        void DeleteMenusInRole(Guid id);
    }
}
