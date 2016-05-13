using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    public partial class DictionaryService : IDictionaryService {

        #region Fields

        private readonly IDictionaryRepository _dictionaryRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DictionaryService(
            IDictionaryRepository dictionaryRepository,
            ICacheManager cacheManager) {
            this._dictionaryRepository = dictionaryRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public Dictionary GetDictionary(int dictionaryId) {
            return _dictionaryRepository.GetEntity(dictionaryId);
        }

        public IPagedList<Dictionary> GetDictionaries(int pageIndex = 0, int pageSize = int.MaxValue) {
            var result = _dictionaryRepository.GetEntities();
            return new PagedList<Dictionary>(result, pageIndex, pageSize);
        }

        public void UpdateDictionary(Dictionary dictionary) {
            if(dictionary == null)
                throw new ArgumentNullException("dictionary");

            _dictionaryRepository.UpdateEntity(dictionary);
        }

        public void UpdateDictionaries(List<Dictionary> dictionaries) {
            if(dictionaries == null)
                throw new ArgumentNullException("dictionaries");

            _dictionaryRepository.UpdateEntities(dictionaries);
        }

        #endregion

    }
}
