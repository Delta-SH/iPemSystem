using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Core.Enum;
using iPem.Data.Repository.Sc;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class EntitiesInRoleService : IEntitiesInRoleService {

        #region Fields

        private readonly IU_EntitiesInRoleRepository _repository;
        private readonly IU_MenuRepository _menuRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public EntitiesInRoleService(
            IU_EntitiesInRoleRepository repository,
            IU_MenuRepository menuRepository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._menuRepository = menuRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public U_EntitiesInRole GetEntitiesInRole(Guid id) {
            if (id == U_Role.SuperId) {
                var key = GlobalCacheKeys.SSH_Authorizations;
                if (_cacheManager.IsSet(key)) {
                    return _cacheManager.Get<U_EntitiesInRole>(key);
                } else {
                    var auth = new U_EntitiesInRole { RoleId = id, Menus = new List<int>(), Permissions = new List<EnmPermission>(), Areas = new List<string>() };
                    foreach (var menu in _menuRepository.GetMenus()) {
                        auth.Menus.Add(menu.Id);
                    }
                    foreach (EnmPermission permission in Enum.GetValues(typeof(EnmPermission))) {
                        auth.Permissions.Add(permission);
                    }
                    _cacheManager.Set(key, auth);
                    return auth;
                }
            } else {
                var key = string.Format(GlobalCacheKeys.SSH_AuthorizationsPattern, id);
                if (_cacheManager.IsSet(key)) {
                    return _cacheManager.Get<U_EntitiesInRole>(key);
                } else {
                    var data = _repository.GetEntitiesInRole(id);
                    _cacheManager.Set(key, data);
                    return data;
                }
            }
        }

        public void Add(U_EntitiesInRole entities) {
            var key = string.Format(GlobalCacheKeys.SSH_AreasPattern, entities.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.SSH_AuthorizationsPattern, entities.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _repository.Insert(entities);
        }

        public void Remove(Guid id) {
            var key = string.Format(GlobalCacheKeys.SSH_AreasPattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.SSH_AuthorizationsPattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _repository.Delete(id);
        }

        #endregion

    }
}