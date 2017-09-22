using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class DictionaryService : IDictionaryService {

        #region Fields

        private readonly IM_DictionaryRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DictionaryService(
            IM_DictionaryRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public M_Dictionary GetDictionary(int id) {
            return _repository.GetDictionary(id);
        }

        public List<M_Dictionary> GetDictionaries() {
            return _repository.GetDictionaries();
        }

        public IPagedList<M_Dictionary> GetPagedDictionaries(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_Dictionary>(this.GetDictionaries(), pageIndex, pageSize);
        }

        public void Update(params M_Dictionary[] dictionaries) {
            if (dictionaries == null || dictionaries.Length == 0)
                throw new ArgumentNullException("dictionaries");

            if (_cacheManager.IsSet(GlobalCacheKeys.Dictionary_Ws))
                _cacheManager.Remove(GlobalCacheKeys.Dictionary_Ws);

            if (_cacheManager.IsSet(GlobalCacheKeys.Dictionary_Ts))
                _cacheManager.Remove(GlobalCacheKeys.Dictionary_Ts);

            if (_cacheManager.IsSet(GlobalCacheKeys.Dictionary_Rt))
                _cacheManager.Remove(GlobalCacheKeys.Dictionary_Rt);

            _repository.Update(dictionaries);
        }

        #endregion

    }
}
