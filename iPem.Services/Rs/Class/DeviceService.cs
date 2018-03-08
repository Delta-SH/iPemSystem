using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            return _repository.GetDevice(id);
        }

        public List<D_Device> GetDevicesInStation(string id) {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            return _repository.GetDevicesInStation(id);
        }

        public List<D_Device> GetDevicesInRoom(string id) {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            return _repository.GetDevicesInRoom(id);
        }

        public List<D_Device> GetDevicesInFsu(string id) {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            return _repository.GetDevicesInFsu(id);
        }

        public List<D_Device> GetDevices() {
            var key = GlobalCacheKeys.Rs_DevicesRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetItemsFromList<D_Device>(key).ToList();
            } else {
                var data = _repository.GetDevices();
                _cacheManager.AddItemsToList(key, data);
                return data;
            }
        }

        public HashSet<string> GetDeviceKeysWithPoints(string[] points) {
            if (points == null || points.Length == 0) 
                throw new ArgumentNullException("points");

            return _repository.GetDeviceKeysWithPoints(points);
        }

        public IPagedList<D_Device> GetPagedDevices(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<D_Device>(this.GetDevices(), pageIndex, pageSize);
        }

        #endregion

    }
}
