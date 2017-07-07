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

        public List<H_IDevice> GetDevices(DateTime date) {
            return _repository.GetDevices(date);
        }

        public IPagedList<H_IDevice> GetPagedDevices(DateTime date, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_IDevice>(this.GetDevices(date), pageIndex, pageSize);
        }

        #endregion

    }
}
