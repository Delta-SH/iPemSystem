using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    /// <summary>
    /// Menu service
    /// </summary>
    public partial class MenuService : IMenuService {
        #region Fields

        private readonly IMenuRepository _menuRepository;
        private readonly IMenusInRoleRepository _menusInRoleRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public MenuService(
            IMenuRepository menuRepository,
            IMenusInRoleRepository menusInRoleRepository,
            ICacheManager cacheManager) {
            this._menuRepository = menuRepository;
            this._menusInRoleRepository = menusInRoleRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a menu by id
        /// </summary>
        /// <param name="id">menu id</param>
        /// <returns>menu</returns>
        public Menu GetMenu(int id) {
            return _menuRepository.GetEntity(id);
        }

        /// <summary>
        /// Gets all menus
        /// </summary>
        /// <param name="roleId">role id</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>menu collection</returns>
        public IPagedList<Menu> GetAllMenus(Guid roleId, int pageIndex, int pageSize) {
            var key = string.Format(GlobalCacheKeys.Cs_MenusInRolePattern, roleId);

            IList<Menu> menus = null;
            if(_cacheManager.IsSet(key)) {
                menus = _cacheManager.Get<IList<Menu>>(key);
            } else {
                menus = roleId.Equals(Role.SuperId) ? _menuRepository.GetEntities() : _menusInRoleRepository.GetEntity(roleId).Menus;
                _cacheManager.Set<IList<Menu>>(key, menus);
            }

            return new PagedList<Menu>(menus, pageIndex, pageSize);
        }

        /// <summary>
        /// Inserts a roles
        /// </summary>
        /// <param name="menu">menu</param>
        public void InsertMenu(Menu menu) {
            if(menu == null)
                throw new ArgumentNullException("menu");

            var key = string.Format(GlobalCacheKeys.Cs_MenusInRolePattern, Role.SuperId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _menuRepository.Insert(menu);
        }

        /// <summary>
        /// Updates the menu
        /// </summary>
        /// <param name="menu">menu</param>
        public void UpdateMenu(Menu menu) {
            if(menu == null)
                throw new ArgumentNullException("menu");

            var key = string.Format(GlobalCacheKeys.Cs_MenusInRolePattern, Role.SuperId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _menuRepository.Update(menu);
        }

        /// <summary>
        /// Marks menu as deleted 
        /// </summary>
        /// <param name="menu">menu</param>
        public void DeleteMenu(Menu menu) {
            if(menu == null)
                throw new ArgumentNullException("menu");

            var key = string.Format(GlobalCacheKeys.Cs_MenusInRolePattern, Role.SuperId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _menuRepository.Delete(menu);
        }
        #endregion
    }
}
