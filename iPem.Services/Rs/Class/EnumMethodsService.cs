using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class EnumMethodsService : IEnumMethodsService {

        #region Fields

        private readonly IEnumMethodsRepository _methodsRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public EnumMethodsService(
            IEnumMethodsRepository methodsRepository,
            ICacheManager cacheManager) {
            this._methodsRepository = methodsRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public EnumMethods GetValue(int id) {
            return _methodsRepository.GetEntity(id);
        }

        public IPagedList<EnumMethods> GetAllValues(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<EnumMethods>(this.GetAllValuesAsList(), pageIndex, pageSize);
        }

        public List<EnumMethods> GetAllValuesAsList() {
            return _methodsRepository.GetEntities();
        }

        public IPagedList<EnumMethods> GetValues(EnmMethodType type, string comment, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<EnumMethods>(this.GetValuesAsList(type, comment), pageIndex, pageSize);
        }

        public List<EnumMethods> GetValuesAsList(EnmMethodType type, string comment) {
            return _methodsRepository.GetEntities(type, comment);
        }

        #endregion

    }
}
