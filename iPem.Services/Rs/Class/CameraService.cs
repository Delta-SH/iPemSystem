using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Rs {
    public partial class CameraService : ICameraService {

        #region Fields

        private readonly IV_CameraRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public CameraService(
            IV_CameraRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public V_Camera GetCamera(string id) {
            return _repository.GetEntity(id);
        }

        public List<V_Camera> GetCameras() {
            var key = GlobalCacheKeys.Rs_CamerasRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetItemsFromList<V_Camera>(key).ToList();
            } else {
                var data = _repository.GetEntities();
                _cacheManager.AddItemsToList(key, data);
                return data;
            }
        }

        public IPagedList<V_Camera> GetPagedCameras(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Camera>(this.GetCameras(), pageIndex, pageSize);
        }

        #endregion

    }
}
