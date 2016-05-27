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

        public IPagedList<IdValuePair<string, string>> GetAllPointsInProtcol(int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _pointsInProtcolRepository.GetEntities();
            return new PagedList<IdValuePair<string, string>>(result, pageIndex, pageSize);
        }

        public IPagedList<IdValuePair<string, string>> GetRelation(int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _pointsInProtcolRepository.GetRelation();
            return new PagedList<IdValuePair<string, string>>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
