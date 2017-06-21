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
                var menus = _menuRepository.GetMenus();
                var permissions = new List<EnmPermission>();
                foreach (EnmPermission permission in Enum.GetValues(typeof(EnmPermission))) {
                    permissions.Add(permission);
                }
                return new U_EntitiesInRole { RoleId = id, Menus = menus, Permissions = permissions, Areas = new List<string>() };
            }

            return _repository.GetEntitiesInRole(id);
        }

        public void Add(U_EntitiesInRole entities) {
            var key = string.Format(GlobalCacheKeys.SSH_AreasPattern, entities.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.SSH_StationsPattern, entities.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.SSH_RoomsPattern, entities.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.SSH_FsusPattern, entities.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.SSH_DevicesPattern, entities.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _repository.Insert(entities);
        }

        public void Remove(Guid id) {
            var key = string.Format(GlobalCacheKeys.SSH_AreasPattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.SSH_StationsPattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.SSH_RoomsPattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.SSH_FsusPattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.SSH_DevicesPattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _repository.Delete(id);
        }

        #endregion

    }
}