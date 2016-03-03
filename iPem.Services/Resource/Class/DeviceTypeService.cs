using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial class DeviceTypeService : IDeviceTypeService {

        #region Fields

        private readonly IDeviceTypeRepository _deviceTypeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DeviceTypeService(
            IDeviceTypeRepository deviceTypeRepository,
            ICacheManager cacheManager) {
            this._deviceTypeRepository = deviceTypeRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public DeviceType GetDeviceType(int id) {
            return _deviceTypeRepository.GetEntity(id);
        }

        public IPagedList<DeviceType> GetAllDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<DeviceType> deviceTypes = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_DeviceTypeRepository)) {
                deviceTypes = _cacheManager.Get<List<DeviceType>>(GlobalCacheKeys.Rs_DeviceTypeRepository);
            } else {
                deviceTypes = _deviceTypeRepository.GetEntities();
                _cacheManager.Set<List<DeviceType>>(GlobalCacheKeys.Rs_DeviceTypeRepository, deviceTypes);
            }

            var result = new PagedList<DeviceType>(deviceTypes, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}
