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
            if(areas == null)
                throw new ArgumentException("areas");

            var key = string.Format(GlobalCacheKeys.Cs_AreasInRolePattern, areas.RoleId);
            if(_cacheManager.IsSet(key)) 
                _cacheManager.Remove(key);

            _areaInRoleRepository.Insert(areas);
        }

        public void DeleteAreasInRole(Guid id) {
            var key = string.Format(GlobalCacheKeys.Cs_AreasInRolePattern, id);
            if(_cacheManager.IsSet(key)) 
                _cacheManager.Remove(key);

            _areaInRoleRepository.Delete(id);
        }

        #endregion

    }
}
