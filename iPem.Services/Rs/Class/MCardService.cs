using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class MCardService : IMCardService {

        #region Fields

        private readonly IM_CardRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public MCardService(
            IM_CardRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public M_Card GetCard(string id) {
            return _repository.GetEntity(id);
        }

        public List<M_Card> GetCards() {
            var key = GlobalCacheKeys.Rs_CardsRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.Get<List<M_Card>>(key);
            } else {
                var data = _repository.GetEntities();
                _cacheManager.Set(key, data);
                return data;
            }
        }

        #endregion
        
    }
}
