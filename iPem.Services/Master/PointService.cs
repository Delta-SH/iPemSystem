using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Core.Enum;
using iPem.Data.Repository.Master;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    public partial class PointService : IPointService {

        #region Fields

        private readonly IPointRepository _pointRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public PointService(
            IPointRepository pointRepository,
            ICacheManager cacheManager) {
            this._pointRepository = pointRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<Point> GetPointsByType(EnmNode nodeType, int pageIndex = 0, int pageSize = int.MaxValue) {
            var key = string.Format(GlobalCacheKeys.Cs_PointsInTypePattern, (int)nodeType);

            List<Point> points = null;
            if(_cacheManager.IsSet(key)) {
                points = _cacheManager.Get<List<Point>>(key);
            } else {
                points = _pointRepository.GetEntities(nodeType);
                _cacheManager.Set<List<Point>>(key, points);
            }

            var result = new PagedList<Point>(points, pageIndex, pageSize);
            return result;
        }

        public IPagedList<Point> GetPointsByProtcol(int protcol, int pageIndex = 0, int pageSize = int.MaxValue) {
            var key = string.Format(GlobalCacheKeys.Cs_PointsInProtcolPattern, protcol);

            List<Point> points = null;
            if(_cacheManager.IsSet(key)) {
                points = _cacheManager.Get<List<Point>>(key);
            } else {
                points = _pointRepository.GetEntities(protcol);
                _cacheManager.Set<List<Point>>(key, points);
            }

            var result = new PagedList<Point>(points, pageIndex, pageSize);
            return result;
        }

        public IPagedList<Point> GetPoints(int protcol, EnmNode nodeType, int pageIndex = 0, int pageSize = int.MaxValue) {
            var key = string.Format(GlobalCacheKeys.Cs_PointsInProtcolAndTypePattern, protcol, (int)nodeType);

            List<Point> points = null;
            if(_cacheManager.IsSet(key)) {
                points = _cacheManager.Get<List<Point>>(key);
            } else {
                points = _pointRepository.GetEntities(protcol, nodeType);
                _cacheManager.Set<List<Point>>(key, points);
            }

            var result = new PagedList<Point>(points, pageIndex, pageSize);
            return result;
        }

        public IPagedList<Point> GetPoints(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Point> points = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_PointsRepository)) {
                points = _cacheManager.Get<List<Point>>(GlobalCacheKeys.Cs_PointsRepository);
            } else {
                points = _pointRepository.GetEntities();
                _cacheManager.Set<List<Point>>(GlobalCacheKeys.Cs_PointsRepository, points);
            }

            var result = new PagedList<Point>(points, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}
