using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class EnumMethodService : IEnumMethodService {

        #region Fields

        private readonly IC_EnumMethodRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public EnumMethodService(
            IC_EnumMethodRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_EnumMethod GetEnumById(int id) {
            return _repository.GetEnumById(id);
        }

        public List<C_EnumMethod> GetEnumsByType(EnmMethodType type, string comment) {
            return _repository.GetEnumsByType(type, comment);
        }

        public List<C_EnumMethod> GetEnums() {
            return _repository.GetEnums();
        }

        public IPagedList<C_EnumMethod> GetPagedEnums(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_EnumMethod>(this.GetEnums(), pageIndex, pageSize);
        }

        #endregion

    }
}
