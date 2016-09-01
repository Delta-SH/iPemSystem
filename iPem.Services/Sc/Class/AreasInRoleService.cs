using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class AreasInRoleService : IAreasInRoleService {

        #region Fields

        private readonly IAreasInRoleRepository _areaInRoleRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AreasInRoleService(
            IAreasInRoleRepository areaInRoleRepository,
            ICacheManager cacheManager) {
            this._areaInRoleRepository = areaInRoleRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<string> GetKeys(Guid role, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<string>(this.GetKeysAsList(role), pageIndex, pageSize);
        }

        public List<string> GetKeysAsList(Guid role) {
            return _areaInRoleRepository.GetEntities(role);
        }

        public void Add(Guid role, List<string> keys) {
            var key = string.Format(GlobalCacheKeys.Og_RoleAreasPattern, role);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Og_RoleStationsPattern, role);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Og_RoleRoomsPattern, role);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Og_RoleFsusPattern, role);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Og_RoleDevicesPattern, role);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _areaInRoleRepository.Insert(role, keys);
        }

        public void Remove(Guid role) {
            var key = string.Format(GlobalCacheKeys.Og_RoleAreasPattern, role);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Og_RoleStationsPattern, role);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Og_RoleRoomsPattern, role);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Og_RoleFsusPattern, role);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Og_RoleDevicesPattern, role);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _areaInRoleRepository.Delete(role);
        }

        #endregion

    }
}