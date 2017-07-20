using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class SDBScriptService : ISDBScriptService {

        #region Fields

        private readonly IS_DBScriptRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public SDBScriptService(
            IS_DBScriptRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<S_DBScript> GetEntities() {
            return _repository.GetEntities();
        }

        public void Add(params S_DBScript[] scripts) {
            if (scripts == null || scripts.Length == 0)
                throw new ArgumentNullException("scripts");

            _repository.Insert(scripts);
        }

        public void Update(params S_DBScript[] scripts) {
            if (scripts == null || scripts.Length == 0)
                throw new ArgumentNullException("scripts");

            _repository.Update(scripts);
        }

        public void Remove(params string[] ids) {
            if (ids == null || ids.Length == 0)
                throw new ArgumentNullException("ids");

            _repository.Delete(ids);
        }

        public IPagedList<S_DBScript> GetPagedDBScripts(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<S_DBScript>(this.GetEntities(), pageIndex, pageSize);
        }

        #endregion

    }
}
