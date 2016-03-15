using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial class DeviceStatusService : IDeviceStatusService {

        #region Fields

        private readonly IDeviceStatusRepository _deviceStatusRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DeviceStatusService(
            IDeviceStatusRepository deviceStatusRepository,
            ICacheManager cacheManager) {
            this._deviceStatusRepository = deviceStatusRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public DeviceStatus GetDeviceStatus(int id) {
            return _deviceStatusRepository.GetEntity(id);
        }

        public IPagedList<DeviceStatus> GetAllDeviceStatus(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<DeviceStatus> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_DeviceStatusRepository)) {
                result = _cacheManager.Get<List<DeviceStatus>>(GlobalCacheKeys.Rs_DeviceStatusRepository);
            } else {
                result = _deviceStatusRepository.GetEntities();
                _cacheManager.Set<List<DeviceStatus>>(GlobalCacheKeys.Rs_DeviceStatusRepository, result);
            }

            return new PagedList<DeviceStatus>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
