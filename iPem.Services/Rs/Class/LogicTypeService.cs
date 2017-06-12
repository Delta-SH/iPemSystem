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

        private readonly IC_LogicTypeRepository _logicTypeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public LogicTypeService(
            IC_LogicTypeRepository logicTypeRepository,
            ICacheManager cacheManager) {
            this._logicTypeRepository = logicTypeRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_LogicType GetLogicType(string id) {
            return _logicTypeRepository.GetEntity(id);
        }

        public C_SubLogicType GetSubLogicType(string id) {
            return _logicTypeRepository.GetSubEntity(id);
        }

        public IPagedList<C_LogicType> GetAllLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_LogicType>(this.GetAllLogicTypesAsList(), pageIndex, pageSize);
        }

        public List<C_LogicType> GetAllLogicTypesAsList() {
            List<C_LogicType> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_LogicTypesRepository)) {
                result = _cacheManager.Get<List<C_LogicType>>(GlobalCacheKeys.Rs_LogicTypesRepository);
            } else {
                result = _logicTypeRepository.GetEntities();
                _cacheManager.Set<List<C_LogicType>>(GlobalCacheKeys.Rs_LogicTypesRepository, result);
            }

            return result;
        }

        public IPagedList<C_SubLogicType> GetAllSubLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_SubLogicType>(this.GetAllSubLogicTypesAsList(), pageIndex, pageSize);
        }

        public List<C_SubLogicType> GetAllSubLogicTypesAsList() {
            List<C_SubLogicType> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_SubLogicTypesRepository)) {
                result = _cacheManager.Get<List<C_SubLogicType>>(GlobalCacheKeys.Rs_SubLogicTypesRepository);
            } else {
                result = _logicTypeRepository.GetSubEntities();
                _cacheManager.Set<List<C_SubLogicType>>(GlobalCacheKeys.Rs_SubLogicTypesRepository, result);
            }

            return result;
        }

        public IPagedList<C_SubLogicType> GetSubLogicTypes(string parent, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_SubLogicType>(this.GetSubLogicTypesAsList(parent), pageIndex, pageSize);
        }

        public List<C_SubLogicType> GetSubLogicTypesAsList(string parent) {
            return _logicTypeRepository.GetSubEntities(parent);
        }

        #endregion

    }
}
