using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using iPem.Services.Common;
using System;

namespace iPem.Services.Master {
    /// <summary>
    /// Areas in role service
    /// </summary>
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

        public AreasInRole GetAreasInRole(Guid id) {
            return _areaInRoleRepository.GetEntities(id);
        }

        public void AddAreasInRole(AreasInRole areas) {
            if(areas == null) throw new ArgumentException("areas");

            var key = string.Format(GlobalCacheKeys.Rl_AreasResultPattern, areas.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Rl_StationsResultPattern, areas.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Rl_RoomsResultPattern, areas.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Rl_DevicesResultPattern, areas.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Rl_AreaAttributesResultPattern, areas.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Rl_StationAttributesResultPattern, areas.RoleId);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _areaInRoleRepository.Insert(areas);
        }

        public void DeleteAreasInRole(Guid id) {
            var key = string.Format(GlobalCacheKeys.Rl_AreasResultPattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Rl_StationsResultPattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Rl_RoomsResultPattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Rl_DevicesResultPattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Rl_AreaAttributesResultPattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            key = string.Format(GlobalCacheKeys.Rl_StationAttributesResultPattern, id);
            if(_cacheManager.IsSet(key)) _cacheManager.Remove(key);

            _areaInRoleRepository.Delete(id);
        }

        #endregion

    }
}
