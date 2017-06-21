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

        private readonly IU_MenuRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public MenuService(
            IU_MenuRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public U_Menu GetMenu(int id) {
            return _repository.GetMenu(id);
        }

        public List<U_Menu> GetMenus() {
            List<U_Menu> result = null;
            var key = GlobalCacheKeys.Sc_MenusRepository;
            if (_cacheManager.IsSet(key)) {
                result = _cacheManager.Get<List<U_Menu>>(key);
            } else {
                result = _repository.GetMenus();
                _cacheManager.Set<List<U_Menu>>(key, result);
            }

            return result;
        }

        public List<U_Menu> GetMenusInRole(Guid id) {
            if(id.Equals(U_Role.SuperId))
                return this.GetMenus();

            List<U_Menu> result = null;
            var key = string.Format(GlobalCacheKeys.Global_MenusInRolePattern, id);
            if(_cacheManager.IsSet(key)) {
                result = _cacheManager.Get<List<U_Menu>>(key);
            } else {
                result = _repository.GetMenusInRole(id);
                _cacheManager.Set<List<U_Menu>>(key, result, CachedIntervals.Global_Default_Intervals);
            }

            return result;
        }

        public void Add(params U_Menu[] menus) {
            if (menus == null || menus.Length == 0)
                throw new ArgumentNullException("menus");

            if(_cacheManager.IsSet(GlobalCacheKeys.Sc_MenusRepository))
                _cacheManager.Remove(GlobalCacheKeys.Sc_MenusRepository);

            _repository.Insert(menus);
        }

        public void Update(params U_Menu[] menus) {
            if (menus == null || menus.Length == 0)
                throw new ArgumentNullException("menus");

            if(_cacheManager.IsSet(GlobalCacheKeys.Sc_MenusRepository))
                _cacheManager.Remove(GlobalCacheKeys.Sc_MenusRepository);

            _repository.Update(menus);
        }

        public void Remove(params U_Menu[] menus) {
            if (menus == null || menus.Length == 0)
                throw new ArgumentNullException("menus");

            if(_cacheManager.IsSet(GlobalCacheKeys.Sc_MenusRepository))
                _cacheManager.Remove(GlobalCacheKeys.Sc_MenusRepository);

            _repository.Delete(menus);
        }

        #endregion

    }
}
