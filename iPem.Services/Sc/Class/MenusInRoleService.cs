using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class MenusInRoleService : IMenusInRoleService {

        #region Fields

        private readonly IMenusInRoleRepository _menusInRoleRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public MenusInRoleService(
            IMenusInRoleRepository menusInRoleRepository,
            ICacheManager cacheManager) {
            this._menusInRoleRepository = menusInRoleRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public U_EntitiesInRole GetMenusInRole(Guid id) {
            return _menusInRoleRepository.GetEntity(id);
        }

        public void AddMenusInRole(U_EntitiesInRole menus) {
            if(menus == null)
                throw new ArgumentException("menus");

            var key = string.Format(GlobalCacheKeys.Rl_MenusResultPattern, menus.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _menusInRoleRepository.Insert(menus);
        }

        public void DeleteMenusInRole(Guid id) {
            var key = string.Format(GlobalCacheKeys.Rl_MenusResultPattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _menusInRoleRepository.Delete(id);
        }

        #endregion

    }
}
