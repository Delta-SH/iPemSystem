using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
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
            return new PagedList<LogicType>(this.GetAllLogicTypesAsList(), pageIndex, pageSize);
        }

        public List<LogicType> GetAllLogicTypesAsList() {
            List<LogicType> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_LogicTypesRepository)) {
                result = _cacheManager.Get<List<LogicType>>(GlobalCacheKeys.Rs_LogicTypesRepository);
            } else {
                result = _logicTypeRepository.GetEntities();
                _cacheManager.Set<List<LogicType>>(GlobalCacheKeys.Rs_LogicTypesRepository, result);
            }

            return result;
        }

        public IPagedList<SubLogicType> GetAllSubLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<SubLogicType>(this.GetAllSubLogicTypesAsList(), pageIndex, pageSize);
        }

        public List<SubLogicType> GetAllSubLogicTypesAsList() {
            List<SubLogicType> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_SubLogicTypesRepository)) {
                result = _cacheManager.Get<List<SubLogicType>>(GlobalCacheKeys.Rs_SubLogicTypesRepository);
            } else {
                result = _logicTypeRepository.GetSubEntities();
                _cacheManager.Set<List<SubLogicType>>(GlobalCacheKeys.Rs_SubLogicTypesRepository, result);
            }

            return result;
        }

        public IPagedList<SubLogicType> GetSubLogicTypes(string parent, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<SubLogicType>(this.GetSubLogicTypesAsList(parent), pageIndex, pageSize);
        }

        public List<SubLogicType> GetSubLogicTypesAsList(string parent) {
            return _logicTypeRepository.GetSubEntities(parent);
        }

        #endregion

    }
}
