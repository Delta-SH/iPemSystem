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
            var key = string.Format(GlobalCacheKeys.Rs_DevicesInParentPattern, parent);

            List<Device> devices = null;
            if(_cacheManager.IsSet(key)) {
                devices = _cacheManager.Get<List<Device>>(key);
            } else {
                devices = _deviceRepository.GetEntities(parent);
                _cacheManager.Set<List<Device>>(key, devices);
            }

            var result = new PagedList<Device>(devices, pageIndex, pageSize);
            return result;
        }

        public IPagedList<Device> GetAllDevices(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Device> devices = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_DevicesRepository)) {
                devices = _cacheManager.Get<List<Device>>(GlobalCacheKeys.Rs_DevicesRepository);
            } else {
                devices = _deviceRepository.GetEntities();
                _cacheManager.Set<List<Device>>(GlobalCacheKeys.Rs_DevicesRepository, devices);
            }

            var result = new PagedList<Device>(devices, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}
