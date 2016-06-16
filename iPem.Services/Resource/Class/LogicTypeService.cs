using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial class LogicTypeService : ILogicTypeService {

        #region Fields

        private readonly ILogicTypeRepository _logicTypeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public LogicTypeService(
            ILogicTypeRepository logicTypeRepository,
            ICacheManager cacheManager) {
            this._logicTypeRepository = logicTypeRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public LogicType GetLogicType(string id) {
            return _logicTypeRepository.GetEntity(id);
        }

        public SubLogicType GetSubLogicType(string id) {
            return _logicTypeRepository.GetSubEntity(id);
        }

        public IPagedList<LogicType> GetAllLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<LogicType> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_LogicTypesRepository)) {
                result = _cacheManager.Get<List<LogicType>>(GlobalCacheKeys.Rs_LogicTypesRepository);
            } else {
                result = _logicTypeRepository.GetEntities();
                _cacheManager.Set<List<LogicType>>(GlobalCacheKeys.Rs_LogicTypesRepository, result);
            }

            return new PagedList<LogicType>(result, pageIndex, pageSize);
        }

        public IPagedList<SubLogicType> GetSubLogicTypes(string parent, int pageIndex = 0, int pageSize = int.MaxValue) {
            List<SubLogicType> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_SubLogicTypesRepository)) {
                var all = _cacheManager.Get<List<SubLogicType>>(GlobalCacheKeys.Rs_SubLogicTypesRepository);
                result = all.FindAll(l => l.LogicTypeId == parent);
            } else {
                result = _logicTypeRepository.GetSubEntities(parent);
            }

            return new PagedList<SubLogicType>(result, pageIndex, pageSize);
        }

        public IPagedList<SubLogicType> GetAllSubLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<SubLogicType> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_SubLogicTypesRepository)) {
                result = _cacheManager.Get<List<SubLogicType>>(GlobalCacheKeys.Rs_SubLogicTypesRepository);
            } else {
                result = _logicTypeRepository.GetSubEntities();
                _cacheManager.Set<List<SubLogicType>>(GlobalCacheKeys.Rs_SubLogicTypesRepository, result);
            }

            return new PagedList<SubLogicType>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
