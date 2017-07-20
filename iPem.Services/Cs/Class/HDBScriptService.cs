using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HDBScriptService : IHDBScriptService {

        #region Fields

        private readonly IH_DBScriptRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HDBScriptService(
            IH_DBScriptRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<H_DBScript> GetEntities() {
            return _repository.GetEntities();
        }

        public void Add(params H_DBScript[] scripts) {
            if (scripts == null || scripts.Length == 0)
                throw new ArgumentNullException("scripts");

            _repository.Insert(scripts);
        }

        public void Update(params H_DBScript[] scripts) {
            if (scripts == null || scripts.Length == 0)
                throw new ArgumentNullException("scripts");

            _repository.Update(scripts);
        }

        public void Remove(params string[] ids) {
            if (ids == null || ids.Length == 0)
                throw new ArgumentNullException("ids");

            _repository.Delete(ids);
        }

        public IPagedList<H_DBScript> GetPagedDBScripts(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_DBScript>(this.GetEntities(), pageIndex, pageSize);
        }

        #endregion

    }
}
