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

        public LogicType GetLogicType(int id) {
            return _logicTypeRepository.GetEntity(id);
        }

        public IPagedList<LogicType> GetAllLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<LogicType> logicTypes = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_LogicTypesRepository)) {
                logicTypes = _cacheManager.Get<List<LogicType>>(GlobalCacheKeys.Rs_LogicTypesRepository);
            } else {
                logicTypes = _logicTypeRepository.GetEntities();
                _cacheManager.Set<List<LogicType>>(GlobalCacheKeys.Rs_LogicTypesRepository, logicTypes);
            }

            var result = new PagedList<LogicType>(logicTypes, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}
