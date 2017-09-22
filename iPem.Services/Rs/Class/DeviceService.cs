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
            return _repository.GetDevice(id);
        }

        public List<D_Device> GetDevicesInRoom(string id) {
            var key = GlobalCacheKeys.Rs_DevicesRepository;
            if (!_cacheManager.IsSet(key)) {
                return this.GetDevices().FindAll(c => c.RoomId == id);
            }

            if (_cacheManager.IsHashSet(key, id)) {
                return _cacheManager.GetFromHash<List<D_Device>>(key, id);
            } else {
                var data = _repository.GetDevicesInRoom(id);
                _cacheManager.SetInHash(key, id, data);
                return data;
            }
        }

        public List<D_Device> GetDevicesInFsu(string id) {
            var key = GlobalCacheKeys.Rs_DevicesRepository;
            if (!_cacheManager.IsSet(key)) {
                return this.GetDevices().FindAll(c => c.FsuId == id);
            }

            return _repository.GetDevicesInFsu(id);
        }

        public List<D_Device> GetDevices() {
            var key = GlobalCacheKeys.Rs_DevicesRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetAllFromHash<List<D_Device>>(key).SelectMany(d => d).ToList();
            } else {
                var data = _repository.GetDevices();
                var caches = data.GroupBy(d => d.RoomId).Select(d => new KeyValuePair<string, object>(d.Key, d.ToList()));
                _cacheManager.SetRangeInHash(key, caches);
                return data;
            }
        }

        public IPagedList<D_Device> GetPagedDevices(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<D_Device>(this.GetDevices(), pageIndex, pageSize);
        }

        #endregion

    }
}
