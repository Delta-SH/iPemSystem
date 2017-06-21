using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

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
            List<C_DeviceType> result = null;
            var key = GlobalCacheKeys.Rs_DeviceTypeRepository;
            if (_cacheManager.IsSet(key)) {
                result = _cacheManager.Get<List<C_DeviceType>>(key);
            } else {
                result = _repository.GetDeviceTypes();
                _cacheManager.Set<List<C_DeviceType>>(key, result);
            }

            return result;
        }

        public List<C_SubDeviceType> GetSubDeviceTypes() {
            List<C_SubDeviceType> result = null;
            var key = GlobalCacheKeys.Rs_SubDeviceTypesRepository;
            if (_cacheManager.IsSet(key)) {
                result = _cacheManager.Get<List<C_SubDeviceType>>(key);
            } else {
                result = _repository.GetSubDeviceTypes();
                _cacheManager.Set<List<C_SubDeviceType>>(key, result);
            }

            return result;
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
