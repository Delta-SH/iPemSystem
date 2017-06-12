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

        private readonly ID_DeviceRepository _deviceRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DeviceService(
            ID_DeviceRepository deviceRepository,
            ICacheManager cacheManager) {
            this._deviceRepository = deviceRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public D_Device GetDevice(string id) {
            return _deviceRepository.GetEntity(id);
        }

        public IPagedList<D_Device> GetAllDevices(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<D_Device>(this.GetAllDevicesAsList(), pageIndex, pageSize);
        }

        public List<D_Device> GetAllDevicesAsList() {
            return _deviceRepository.GetDevices();
        }

        public IPagedList<D_Device> GetDevices(string parent, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<D_Device>(this.GetDevicesAsList(parent), pageIndex, pageSize);
        }

        public List<D_Device> GetDevicesAsList(string parent) {
            return _deviceRepository.GetDevices(parent);
        }

        #endregion

    }
}
