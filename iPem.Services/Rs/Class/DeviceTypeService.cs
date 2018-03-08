using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Rs {
    public partial class DeviceTypeService : IDeviceTypeService {

        #region Fields

        private readonly IC_DeviceTypeRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DeviceTypeService(
            IC_DeviceTypeRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_DeviceType GetDeviceType(string id) {
            return _repository.GetDeviceType(id);
        }

        public C_SubDeviceType GetSubDeviceType(string id) {
            return _repository.GetSubDeviceType(id);
        }

        public List<C_DeviceType> GetDeviceTypes() {
            var key = GlobalCacheKeys.Rs_DeviceTypeRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetItemsFromList<C_DeviceType>(key).ToList();
            } else {
                var data = _repository.GetDeviceTypes();
                _cacheManager.AddItemsToList(key, data);
                return data;
            }
        }

        public List<C_SubDeviceType> GetSubDeviceTypes() {
            var key = GlobalCacheKeys.Rs_SubDeviceTypesRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetItemsFromList<C_SubDeviceType>(key).ToList();
            } else {
                var data = _repository.GetSubDeviceTypes();
                _cacheManager.AddItemsToList(key, data);
                return data;
            }
        }

        public IPagedList<C_DeviceType> GetPagedDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_DeviceType>(this.GetDeviceTypes(), pageIndex, pageSize);
        }

        public IPagedList<C_SubDeviceType> GetPagedSubDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_SubDeviceType>(this.GetSubDeviceTypes(), pageIndex, pageSize);
        }

        #endregion

    }
}
