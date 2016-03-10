using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial class SubDeviceTypeService : ISubDeviceTypeService {

        #region Fields

        private readonly ISubDeviceTypeRepository _subDeviceTypeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public SubDeviceTypeService(
            ISubDeviceTypeRepository subDeviceTypeRepository,
            ICacheManager cacheManager) {
            this._subDeviceTypeRepository = subDeviceTypeRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public SubDeviceType GetSubDeviceType(int id) {
            return _subDeviceTypeRepository.GetEntity(id);
        }

        public IPagedList<SubDeviceType> GetAllSubDeviceTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<SubDeviceType> subDeviceTypes = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_SubDeviceTypesRepository)) {
                subDeviceTypes = _cacheManager.Get<List<SubDeviceType>>(GlobalCacheKeys.Rs_SubDeviceTypesRepository);
            } else {
                subDeviceTypes = _subDeviceTypeRepository.GetEntities();
                _cacheManager.Set<List<SubDeviceType>>(GlobalCacheKeys.Rs_SubDeviceTypesRepository, subDeviceTypes);
            }

            var result = new PagedList<SubDeviceType>(subDeviceTypes, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}