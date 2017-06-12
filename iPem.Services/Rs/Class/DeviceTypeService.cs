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

        private readonly IC_DeviceTypeRepository _typeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DeviceTypeService(
            IC_DeviceTypeRepository typeRepository,
            ICacheManager cacheManager) {
            this._typeRepository = typeRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_DeviceType GetDeviceType(string id) {
            return _typeRepository.GetEntity(id);
        }

        public C_SubDeviceType GetSubDeviceType(string id) {
            return _typeRepository.GetSubEntity(id);
        }

        public IPagedList<C_DeviceType> GetAllDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_DeviceType>(this.GetAllDeviceTypesAsList(), pageIndex, pageSize);
        }

        public List<C_DeviceType> GetAllDeviceTypesAsList() {
            List<C_DeviceType> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_DeviceTypeRepository)) {
                result = _cacheManager.Get<List<C_DeviceType>>(GlobalCacheKeys.Rs_DeviceTypeRepository);
            } else {
                result = _typeRepository.GetEntities();
                _cacheManager.Set<List<C_DeviceType>>(GlobalCacheKeys.Rs_DeviceTypeRepository, result);
            }

            return result;
        }

        public IPagedList<C_SubDeviceType> GetAllSubDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_SubDeviceType>(this.GetAllSubDeviceTypesAsList(), pageIndex, pageSize);
        }

        public List<C_SubDeviceType> GetAllSubDeviceTypesAsList() {
            List<C_SubDeviceType> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_SubDeviceTypesRepository)) {
                result = _cacheManager.Get<List<C_SubDeviceType>>(GlobalCacheKeys.Rs_SubDeviceTypesRepository);
            } else {
                result = _typeRepository.GetSubEntities();
                _cacheManager.Set<List<C_SubDeviceType>>(GlobalCacheKeys.Rs_SubDeviceTypesRepository, result);
            }

            return result;
        }

        #endregion

    }
}
