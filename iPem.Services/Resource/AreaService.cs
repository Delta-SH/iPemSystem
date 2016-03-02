using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    /// <summary>
    /// Area service
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

        public Area GetArea(string id) {
            return _areaRepository.GetEntity(id);
        }

        public IPagedList<Area> GetAreas(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Area> areas = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_AreasRepository)) {
                areas = _cacheManager.Get<List<Area>>(GlobalCacheKeys.Rs_AreasRepository);
            } else {
                areas = _areaRepository.GetEntities();
                _cacheManager.Set<List<Area>>(GlobalCacheKeys.Rs_AreasRepository, areas);
            }

            var result = new PagedList<Area>(areas, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}
