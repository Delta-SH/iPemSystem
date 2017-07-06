using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HIDeviceService : IHIDeviceService {

        #region Fields

        private readonly IH_IDeviceRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HIDeviceService(
            IH_IDeviceRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<H_IDevice> GetDevicesInTypeId(string type) {
            return _repository.GetDevicesInTypeId(type);
        }

        public List<H_IDevice> GetDevicesInTypeName(string type) {
            return _repository.GetDevicesInTypeName(type);
        }

        public List<H_IDevice> GetDevicesInParent(string parent) {
            return _repository.GetDevicesInStation(parent);
        }

        public List<H_IDevice> GetDevices() {
            return _repository.GetDevices();
        }

        public IPagedList<H_IDevice> GetPagedDevices(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_IDevice>(this.GetDevices(), pageIndex, pageSize);
        }

        #endregion

    }
}
