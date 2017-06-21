using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class RedefinePointService : IRedefinePointService {

        #region Fields

        private readonly ID_RedefinePointRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public RedefinePointService(
            ID_RedefinePointRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public D_RedefinePoint GetRedefinePoint(string device, string point) {
            return _repository.GetRedefinePoint(device, point);
        }

        public List<D_RedefinePoint> GetRedefinePointsInDevice(string id) {
            return _repository.GetRedefinePointsInDevice(id);
        }

        public List<D_RedefinePoint> GetRedefinePoints() {
            return _repository.GetRedefinePoints();
        }

        public IPagedList<D_RedefinePoint> GetPagedRedefinePoints(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<D_RedefinePoint>(this.GetRedefinePoints(), pageIndex, pageSize);
        }

        #endregion

    }
}
