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

        private readonly ID_DeviceRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DeviceService(
            ID_DeviceRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public D_Device GetDevice(string id) {
            return _repository.GetDevice(id);
        }

        public List<D_Device> GetDevicesInRoom(string id) {
            return _repository.GetDevicesInRoom(id);
        }

        public List<D_Device> GetDevices() {
            return _repository.GetDevices();
        }

        public IPagedList<D_Device> GetPagedDevices(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<D_Device>(this.GetDevices(), pageIndex, pageSize);
        }

        #endregion

    }
}
