using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial class DeviceService : IDeviceService {

        #region Fields

        private readonly IDeviceRepository _deviceRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DeviceService(
            IDeviceRepository deviceRepository,
            ICacheManager cacheManager) {
            this._deviceRepository = deviceRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public Device GetDevice(string id) {
            return _deviceRepository.GetEntity(id);
        }

        public IPagedList<Device> GetDevicesByParent(string parent, int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _deviceRepository.GetEntities(parent);
            return new PagedList<Device>(result, pageIndex, pageSize);
        }

        public IPagedList<Device> GetAllDevices(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Device> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_DevicesRepository)) {
                result = _cacheManager.Get<List<Device>>(GlobalCacheKeys.Rs_DevicesRepository);
            } else {
                result = _deviceRepository.GetEntities();
                _cacheManager.Set<List<Device>>(GlobalCacheKeys.Rs_DevicesRepository, result);
            }

            return new PagedList<Device>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
