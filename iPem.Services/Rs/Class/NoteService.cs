using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class NoteService : INoteService {

        #region Fields

        private readonly IH_NoteRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public NoteService(
            IH_NoteRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<H_Note> GetNotices() {
            return _repository.GetEntities();
        }

        public void Add(params H_Note[] notices) {
            if (notices == null || notices.Length == 0)
                throw new ArgumentNullException("notices");

            _repository.Insert(notices);
        }

        public void Remove(params H_Note[] notices) {
            if (notices == null || notices.Length == 0)
                throw new ArgumentNullException("notices");

            _repository.Delete(notices);
        }

        public void Clear() {
            _repository.Clear();
        }

        #endregion

    }
}
