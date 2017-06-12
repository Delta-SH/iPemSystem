using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
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

        public M_Dictionary GetDictionary(int id) {
            return _dictionaryRepository.GetEntity(id);
        }

        public IPagedList<M_Dictionary> GetDictionaries(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<M_Dictionary>(this.GetDictionariesAsList(), pageIndex, pageSize);
        }

        public List<M_Dictionary> GetDictionariesAsList() {
            return _dictionaryRepository.GetEntities();
        }

        public void Update(M_Dictionary dictionary) {
            if(dictionary == null)
                throw new ArgumentNullException("dictionary");

            _dictionaryRepository.Update(dictionary);
        }

        public void Update(List<M_Dictionary> dictionaries) {
            if(dictionaries == null)
                throw new ArgumentNullException("dictionaries");

            _dictionaryRepository.Update(dictionaries);
        }

        #endregion

    }
}
