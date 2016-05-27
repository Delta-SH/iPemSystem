using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.History;
using iPem.Data.Repository.History;
using System;
using System.Collections.Generic;

namespace iPem.Services.History {
    public partial class ActValueService : IActValueService {

        #region Fields

        private readonly IActValueRepository _actRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ActValueService(
            IActValueRepository actRepository,
            ICacheManager cacheManager) {
            this._actRepository = actRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<ActValue> GetActValues(string device, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _actRepository.GetEntities(device);
            return new PagedList<ActValue>(result, pageIndex, pageSize);
        }

        public IPagedList<ActValue> GetActValues(string[] devices, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _actRepository.GetEntities(devices);
            return new PagedList<ActValue>(result, pageIndex, pageSize);
        }

        public IPagedList<ActValue> GetAllActValues(int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _actRepository.GetEntities();
            return new PagedList<ActValue>(result, pageIndex, pageSize);
        }

        public void AddValue(ActValue value) {
            _actRepository.Insert(value);
        }

        public void AddValues(List<ActValue> values) {
            _actRepository.Insert(values);
        }

        public void Clear() {
            _actRepository.Clear();
        }

        #endregion

    }
}
