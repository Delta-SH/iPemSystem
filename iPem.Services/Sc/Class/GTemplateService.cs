using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class GTemplateService : IGTemplateService {

        #region Fields

        private readonly IG_TemplateRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public GTemplateService(
            IG_TemplateRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public G_Template GetTemplate(string name) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            return _repository.GetEntity(name);
        }

        public bool Exist(string name) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            return _repository.ExistEntity(name);
        }

        public List<G_Template> GetTemplates() {
            return _repository.GetEntities();
        }

        public List<string> GetNames() {
            return _repository.GetNames();
        }

        public void Add(params G_Template[] entities) {
            if (entities == null)
                throw new ArgumentNullException("entities");

            _repository.Insert(entities);
        }

        public void Update(params G_Template[] entities) {
            if (entities == null)
                throw new ArgumentNullException("entities");

            _repository.Update(entities);
        }

        public void Remove(params string[] names) {
            if (names == null)
                throw new ArgumentNullException("names");

            _repository.Delete(names);
        }

        public void Clear() {
            _repository.Clear();
        }

        #endregion
        
    }
}
