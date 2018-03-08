using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Rs {
    public partial class PointService : IPointService {

        #region Fields

        private readonly IP_PointRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public PointService(
            IP_PointRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<P_Point> GetPointsInProtocol(string id) {
            return _repository.GetPointsInProtocol(id);
        }

        public List<P_Point> GetPoints() {
            var key = GlobalCacheKeys.Rs_PointsRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetItemsFromList<P_Point>(key).ToList();
            } else {
                var data = _repository.GetPoints();
                _cacheManager.AddItemsToList(key, data);
                return data;
            }
        }

        public P_SubPoint GetSubPoint(string point, string statype) {
            return _repository.GetSubPoint(point, statype);
        }

        public List<P_SubPoint> GetSubPointsInPoint(string id) {
            return _repository.GetSubPointsInPoint(id);
        }

        public List<P_SubPoint> GetSubPoints() {
            return _repository.GetSubPoints();
        }

        public IPagedList<P_Point> GetPagedPoints(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<P_Point>(this.GetPoints(), pageIndex, pageSize);
        }

        public IPagedList<P_SubPoint> GetPagedSubPoints(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<P_SubPoint>(this.GetSubPoints(), pageIndex, pageSize);
        }

        #endregion

    }
}
