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
    public partial class AreaService : IAreaService {

        #region Fields

        private readonly IAreaRepository _areaRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AreaService(
            IAreaRepository areaRepository,
            ICacheManager cacheManager) {
            this._areaRepository = areaRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all areas in the role
        /// </summary>
        /// <param name="role">role idetifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>area collection</returns>
        public IPagedList<Area> GetAreasInRole(Guid role, int pageIndex = 0, int pageSize = int.MaxValue) {
            var key = string.Format(GlobalCacheKeys.Cs_AreasInRolePattern, role);

            IList<Area> areas = null;
            if(_cacheManager.IsSet(key)) {
                areas = _cacheManager.Get<IList<Area>>(key);
            } else {
                areas = Role.SuperId.Equals(role) ? _areaRepository.GetEntities() : _areaRepository.GetEntities(role);
                _cacheManager.Set<IList<Area>>(key, areas);
            }

            return new PagedList<Area>(areas, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets all areas
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>area collection</returns>
        public IPagedList<Area> GetAllAreas(int pageIndex = 0, int pageSize = int.MaxValue) {
            IList<Area> areas = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_AreasRepository)) {
                areas = _cacheManager.Get<IList<Area>>(GlobalCacheKeys.Cs_AreasRepository);
            } else {
                areas = _areaRepository.GetEntities();
                _cacheManager.Set<IList<Area>>(GlobalCacheKeys.Cs_AreasRepository, areas);
            }

            return new PagedList<Area>(areas, pageIndex, pageSize);
        }

        /// <summary>
        /// Add an area
        /// </summary>
        /// <param name="area">area</param>
        public void AddArea(Area area) {
            if(area == null)
                throw new ArgumentNullException("area");

            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_AreasRepository)) 
                _cacheManager.Remove(GlobalCacheKeys.Cs_AreasRepository);

            _areaRepository.Insert(area);
        }

        /// <summary>
        /// Marks area as deleted 
        /// </summary>
        /// <param name="area">area</param>
        public void RemoveArea(Area area) {
            if(area == null)
                throw new ArgumentNullException("area");

            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_AreasRepository)) 
                _cacheManager.Remove(GlobalCacheKeys.Cs_AreasRepository);

            _areaRepository.Delete(area);
        }

        #endregion

    }
}
