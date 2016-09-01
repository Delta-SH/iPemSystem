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

        private readonly IDeviceTypeRepository _typeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DeviceTypeService(
            IDeviceTypeRepository typeRepository,
            ICacheManager cacheManager) {
            this._typeRepository = typeRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public DeviceType GetDeviceType(string id) {
            return _typeRepository.GetEntity(id);
        }

        public SubDeviceType GetSubDeviceType(string id) {
            return _typeRepository.GetSubEntity(id);
        }

        public IPagedList<DeviceType> GetAllDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<DeviceType>(this.GetAllDeviceTypesAsList(), pageIndex, pageSize);
        }

        public List<DeviceType> GetAllDeviceTypesAsList() {
            List<DeviceType> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_DeviceTypeRepository)) {
                result = _cacheManager.Get<List<DeviceType>>(GlobalCacheKeys.Rs_DeviceTypeRepository);
            } else {
                result = _typeRepository.GetEntities();
                _cacheManager.Set<List<DeviceType>>(GlobalCacheKeys.Rs_DeviceTypeRepository, result);
            }

            return result;
        }

        public IPagedList<SubDeviceType> GetAllSubDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<SubDeviceType>(this.GetAllSubDeviceTypesAsList(), pageIndex, pageSize);
        }

        public List<SubDeviceType> GetAllSubDeviceTypesAsList() {
            List<SubDeviceType> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_SubDeviceTypesRepository)) {
                result = _cacheManager.Get<List<SubDeviceType>>(GlobalCacheKeys.Rs_SubDeviceTypesRepository);
            } else {
                result = _typeRepository.GetSubEntities();
                _cacheManager.Set<List<SubDeviceType>>(GlobalCacheKeys.Rs_SubDeviceTypesRepository, result);
            }

            return result;
        }

        #endregion

    }
}
