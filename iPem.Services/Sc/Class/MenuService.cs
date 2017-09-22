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
            var key = GlobalCacheKeys.Sc_MenusRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.Get<List<U_Menu>>(key);
            } else {
                var data = _repository.GetMenus();
                _cacheManager.Set(key, data);
                return data;
            }
        }

        public List<U_Menu> GetMenusInRole(Guid id) {
            if (U_Role.SuperId.Equals(id))
                return this.GetMenus();

            return _repository.GetMenusInRole(id);
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
