using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class FollowPointService : IFollowPointService {

        #region Fields

        private readonly IU_FollowPointRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FollowPointService(
            IU_FollowPointRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<U_FollowPoint> GetFollowPointsInUser(Guid id) {
            return _repository.GetFollowPointsInUser(id);
        }

        public List<U_FollowPoint> GetFollowPoints() {
            return _repository.GetFollowPoints();
        }

        public IPagedList<U_FollowPoint> GetPagedFollowPointsInUser(Guid id, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_FollowPoint>(this.GetFollowPointsInUser(id), pageIndex, pageSize);
        }

        public IPagedList<U_FollowPoint> GetPagedFollowPoints(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<U_FollowPoint>(this.GetFollowPoints(), pageIndex, pageSize);
        }

        public void Add(params U_FollowPoint[] points) {
            if (points == null || points.Length == 0)
                throw new ArgumentNullException("points");

            _repository.Insert(points);
        }

        public void Remove(params U_FollowPoint[] points) {
            if (points == null || points.Length == 0)
                throw new ArgumentNullException("points");

            _repository.Delete(points);
        }

        #endregion

    }
}
