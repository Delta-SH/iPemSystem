using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
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

        public IPagedList<Device> GetAllDevices(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Device> devices = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_DevicesRepository)) {
                devices = _cacheManager.Get<List<Device>>(GlobalCacheKeys.Cs_DevicesRepository);
            } else {
                devices = _deviceRepository.GetEntities();
                _cacheManager.Set<List<Device>>(GlobalCacheKeys.Cs_DevicesRepository, devices);
            }

            var result = new PagedList<Device>(devices, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}
