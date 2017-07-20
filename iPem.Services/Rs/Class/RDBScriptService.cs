using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class RDBScriptService : IRDBScriptService {

        #region Fields

        private readonly IR_DBScriptRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public RDBScriptService(
            IR_DBScriptRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<R_DBScript> GetEntities() {
            return _repository.GetEntities();
        }

        public void Add(params R_DBScript[] scripts) {
            if (scripts == null || scripts.Length == 0)
                throw new ArgumentNullException("scripts");

            _repository.Insert(scripts);
        }

        public void Update(params R_DBScript[] scripts) {
            if (scripts == null || scripts.Length == 0)
                throw new ArgumentNullException("scripts");

            _repository.Update(scripts);
        }

        public void Remove(params string[] ids) {
            if (ids == null || ids.Length == 0)
                throw new ArgumentNullException("ids");

            _repository.Delete(ids);
        }

        public IPagedList<R_DBScript> GetPagedDBScripts(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<R_DBScript>(this.GetEntities(), pageIndex, pageSize);
        }

        #endregion

    }
}
