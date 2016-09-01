using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class DeviceKeyService : IDeviceKeyService {

        #region Fields

        private readonly IDeviceKeyRepository _deviceKeyRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DeviceKeyService(
            IDeviceKeyRepository deviceKeyRepository,
            ICacheManager cacheManager) {
            this._deviceKeyRepository = deviceKeyRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<DeviceKey> GetAllKeys(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<DeviceKey>(this.GetAllKeysAsList(), pageIndex, pageSize);
        }

        public List<DeviceKey> GetAllKeysAsList() {
            return _deviceKeyRepository.GetEntities();
        }

        #endregion

    }
}
