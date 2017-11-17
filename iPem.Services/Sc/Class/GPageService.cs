using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class GPageService : IGPageService {

        #region Fields

        private readonly IG_PageRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public GPageService(
            IG_PageRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public G_Page GetPage(string name) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            return _repository.GetEntity(name);
        }

        public bool Exist(string name) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            return _repository.ExistEntity(name);
        }

        public List<G_Page> GetPages() {
            return _repository.GetEntities();
        }

        public List<G_Page> GetPages(string role) {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentNullException("role");

            return _repository.GetEntities(role);
        }

        public List<G_Page> GetPages(string role, string id, int type) {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentNullException("role");

            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            return _repository.GetEntities(role, id, type);
        }

        public List<string> GetNames(string role, string id, int type) {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentNullException("role");

            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            return _repository.GetNames(role, id, type);
        }

        public void Add(params G_Page[] entities) {
            if (entities == null)
                throw new ArgumentNullException("entities");

            _repository.Insert(entities);
        }

        public void Update(params G_Page[] entities) {
            if (entities == null)
                throw new ArgumentNullException("entities");

            _repository.Update(entities);
        }

        public void Remove(params string[] names) {
            if (names == null)
                throw new ArgumentNullException("names");

            _repository.Delete(names);
        }

        public void Clear(string role) {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentNullException("role");

            _repository.Clear(role);
        }

        public void Clear() {
            _repository.Clear();
        }

        #endregion

    }
}
