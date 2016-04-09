using iPem.Core;
using iPem.Core.Caching;
using iPem.Data.Repository.Master;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    public partial class PointsInProtcolService : IPointsInProtcolService {

        #region Fields

        private readonly IPointsInProtcolRepository _pointsInProtcolRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public PointsInProtcolService(
            IPointsInProtcolRepository pointsInProtcolRepository,
            ICacheManager cacheManager) {
            this._pointsInProtcolRepository = pointsInProtcolRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<IdValuePair<int, string>> GetAllPointsInProtcol(int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _pointsInProtcolRepository.GetEntities();
            return new PagedList<IdValuePair<int, string>>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
