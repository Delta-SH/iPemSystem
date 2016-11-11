using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Am;
using iPem.Data.Repository.Am;
using System;
using System.Collections.Generic;

namespace iPem.Services.Am {
    public partial class AmDeviceService : IAmDeviceService {

        #region Fields

        private readonly IAmDeviceRepository _amRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AmDeviceService(
            IAmDeviceRepository amRepository,
            ICacheManager cacheManager) {
            this._amRepository = amRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<AmDevice> GetAmDevices(string type, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<AmDevice>(this.GetAmDevicesAsList(type), pageIndex, pageSize);
        }

        public List<AmDevice> GetAmDevicesAsList(string type) {
            return _amRepository.GetEntities(type);
        }

        public IPagedList<AmDevice> GetAmDevices(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<AmDevice>(this.GetAmDevicesAsList(), pageIndex, pageSize);
        }

        public List<AmDevice> GetAmDevicesAsList() {
            return _amRepository.GetEntities();
        }

        #endregion

    }
}
