using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
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

        public U_Menu GetMenu(int id) {
            return _menuRepository.GetEntity(id);
        }

        public IPagedList<U_Menu> GetAllMenus(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_Menu>(this.GetAllMenusAsList(), pageIndex, pageSize);
        }

        public List<U_Menu> GetAllMenusAsList() {
            List<U_Menu> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Sc_MenusRepository)) {
                result = _cacheManager.Get<List<U_Menu>>(GlobalCacheKeys.Sc_MenusRepository);
            } else {
                result = _menuRepository.GetEntities();
                _cacheManager.Set<List<U_Menu>>(GlobalCacheKeys.Sc_MenusRepository, result);
            }

            return result;
        }

        public IPagedList<U_Menu> GetMenus(Guid role, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_Menu>(this.GetMenusAsList(role), pageIndex, pageSize);
        }

        public List<U_Menu> GetMenusAsList(Guid role) {
            if(role.Equals(U_Role.SuperId))
                return this.GetAllMenusAsList();

            List<U_Menu> result = null;
            var key = string.Format(GlobalCacheKeys.Rl_MenusResultPattern, role);
            if(_cacheManager.IsSet(key)) {
                result = _cacheManager.Get<List<U_Menu>>(key);
            } else {
                result = _menusInRoleRepository.GetEntity(role).Menus;
                _cacheManager.Set<List<U_Menu>>(key, result, CachedIntervals.Global_Intervals);
            }

            return result;
        }

        public void InsertMenu(U_Menu menu) {
            if(menu == null)
                throw new ArgumentNullException("menu");

            if(_cacheManager.IsSet(GlobalCacheKeys.Sc_MenusRepository))
                _cacheManager.Remove(GlobalCacheKeys.Sc_MenusRepository);

            _menuRepository.Insert(menu);
        }

        public void UpdateMenu(U_Menu menu) {
            if(menu == null)
                throw new ArgumentNullException("menu");

            if(_cacheManager.IsSet(GlobalCacheKeys.Sc_MenusRepository))
                _cacheManager.Remove(GlobalCacheKeys.Sc_MenusRepository);

            _menuRepository.Update(menu);
        }

        public void DeleteMenu(U_Menu menu) {
            if(menu == null)
                throw new ArgumentNullException("menu");

            if(_cacheManager.IsSet(GlobalCacheKeys.Sc_MenusRepository))
                _cacheManager.Remove(GlobalCacheKeys.Sc_MenusRepository);

            _menuRepository.Delete(menu);
        }

        #endregion

    }
}
