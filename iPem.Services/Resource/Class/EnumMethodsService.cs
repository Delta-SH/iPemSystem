using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial class EnumMethodsService : IEnumMethodsService {

        #region Fields

        private readonly IEnumMethodsRepository _enumMethodsRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public EnumMethodsService(
            IEnumMethodsRepository enumMethodsRepository,
            ICacheManager cacheManager) {
            this._enumMethodsRepository = enumMethodsRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public EnumMethods GetEnumMethods(string id) {
            return _enumMethodsRepository.GetEntity(id);
        }

        public IPagedList<EnumMethods> GetAllEnumMethods(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<EnumMethods> methods = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_EnumMethodsRepository)) {
                methods = _cacheManager.Get<List<EnumMethods>>(GlobalCacheKeys.Rs_EnumMethodsRepository);
            } else {
                methods = _enumMethodsRepository.GetEntities();
                _cacheManager.Set<List<EnumMethods>>(GlobalCacheKeys.Rs_EnumMethodsRepository, methods);
            }

            var result = new PagedList<EnumMethods>(methods, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}
