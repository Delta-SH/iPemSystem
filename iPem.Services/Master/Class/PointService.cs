using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Core.Enum;
using iPem.Data.Repository.Master;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IPagedList<Point> GetPointsByDevice(string device, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _pointRepository.GetEntitiesByDevice(device);
            return new PagedList<Point>(result, pageIndex, pageSize);
        }

        public IPagedList<Point> GetPointsByType(int[] types, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = new List<Point>();
            foreach(var type in types) {
                result.AddRange(_pointRepository.GetEntitiesByType(type));
            }

            return new PagedList<Point>(result, pageIndex, pageSize);
        }

        public IPagedList<Point> GetPointsByProtcol(int protcol, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _pointRepository.GetEntitiesByProtcol(protcol);
            return new PagedList<Point>(result, pageIndex, pageSize);
        }

        public IPagedList<Point> GetPoints(string device, int[] types, int pageIndex = 0, int pageSize = int.MaxValue) {
            var points = _pointRepository.GetEntitiesByDevice(device);
            var result = points.FindAll(p => types.Contains((int)p.Type));

            return new PagedList<Point>(result, pageIndex, pageSize);
        }

        public IPagedList<Point> GetPoints(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Point> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_PointsRepository)) {
                result = _cacheManager.Get<List<Point>>(GlobalCacheKeys.Cs_PointsRepository);
            } else {
                result = _pointRepository.GetEntities();
                _cacheManager.Set<List<Point>>(GlobalCacheKeys.Cs_PointsRepository, result);
            }

            return new PagedList<Point>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
