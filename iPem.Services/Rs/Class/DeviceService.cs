using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
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

        public IPagedList<Device> GetAllDevices(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Device>(this.GetAllDevicesAsList(), pageIndex, pageSize);
        }

        public List<Device> GetAllDevicesAsList() {
            return _deviceRepository.GetEntities();
        }

        public IPagedList<Device> GetDevices(string parent, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Device>(this.GetDevicesAsList(parent), pageIndex, pageSize);
        }

        public List<Device> GetDevicesAsList(string parent) {
            return _deviceRepository.GetEntities(parent);
        }

        #endregion

    }
}
