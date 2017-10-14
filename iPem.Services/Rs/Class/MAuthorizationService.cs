using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class MAuthorizationService : IMAuthorizationService {

        #region Fields

        private readonly IM_AuthorizationRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public MAuthorizationService(
            IM_AuthorizationRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<M_Authorization> GetAuthorizations() {
            return _repository.GetEntities();
        }

        public List<M_Authorization> GetAuthorizationsInType(EnmEmpType type) {
            return _repository.GetEntitiesInType(type);
        }

        public List<M_Authorization> GetAuthorizationsInCard(string card) {
            return _repository.GetEntitiesInCard(card);
        }

        #endregion

    }
}
