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
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>menu collection</returns>
        public IPagedList<Menu> GetAllMenus(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Menu> menus = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_MenusRepository)) {
                menus = _cacheManager.Get<List<Menu>>(GlobalCacheKeys.Cs_MenusRepository);
            } else {
                menus = _menuRepository.GetEntities();
                _cacheManager.Set<List<Menu>>(GlobalCacheKeys.Cs_MenusRepository, menus);
            }

            return new PagedList<Menu>(menus, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets all menus in the role
        /// </summary>
        /// <param name="role">the role identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>menu collection</returns>
        public IPagedList<Menu> GetMenus(Guid role, int pageIndex = 0, int pageSize = int.MaxValue) {
            if(role.Equals(Role.SuperId))
                return this.GetAllMenus(pageIndex, pageSize);

            List<Menu> menus = null;
            var key = string.Format(GlobalCacheKeys.Rl_MenusResultPattern, role);
            if(_cacheManager.IsSet(key)) {
                menus = _cacheManager.Get<List<Menu>>(key);
            } else {
                menus = _menusInRoleRepository.GetEntity(role).Menus;
                _cacheManager.Set<List<Menu>>(key, menus);
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

            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_MenusRepository))
                _cacheManager.Remove(GlobalCacheKeys.Cs_MenusRepository);

            _menuRepository.Insert(menu);
        }

        /// <summary>
        /// Updates the menu
        /// </summary>
        /// <param name="menu">menu</param>
        public void UpdateMenu(Menu menu) {
            if(menu == null)
                throw new ArgumentNullException("menu");

            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_MenusRepository))
                _cacheManager.Remove(GlobalCacheKeys.Cs_MenusRepository);

            _menuRepository.Update(menu);
        }

        /// <summary>
        /// Marks menu as deleted 
        /// </summary>
        /// <param name="menu">menu</param>
        public void DeleteMenu(Menu menu) {
            if(menu == null)
                throw new ArgumentNullException("menu");

            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_MenusRepository))
                _cacheManager.Remove(GlobalCacheKeys.Cs_MenusRepository);

            _menuRepository.Delete(menu);
        }

        #endregion

    }
}
