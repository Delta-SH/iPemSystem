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

        private readonly IC_EnumMethodRepository _methodsRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public EnumMethodsService(
            IC_EnumMethodRepository methodsRepository,
            ICacheManager cacheManager) {
            this._methodsRepository = methodsRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_EnumMethod GetValue(int id) {
            return _methodsRepository.GetEntity(id);
        }

        public IPagedList<C_EnumMethod> GetAllValues(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_EnumMethod>(this.GetAllValuesAsList(), pageIndex, pageSize);
        }

        public List<C_EnumMethod> GetAllValuesAsList() {
            return _methodsRepository.GetEntities();
        }

        public IPagedList<C_EnumMethod> GetValues(EnmMethodType type, string comment, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_EnumMethod>(this.GetValuesAsList(type, comment), pageIndex, pageSize);
        }

        public List<C_EnumMethod> GetValuesAsList(EnmMethodType type, string comment) {
            return _methodsRepository.GetEntities(type, comment);
        }

        #endregion

    }
}
