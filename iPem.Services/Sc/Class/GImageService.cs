using iPem.Core.Caching;
using iPem.Core.Domain.Sc;
using iPem.Data.Repository.Sc;
using System;
using System.Collections.Generic;

namespace iPem.Services.Sc {
    public partial class GImageService : IGImageService {

        #region Fields

        private readonly IG_ImageRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public GImageService(
            IG_ImageRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public G_Image GetImage(string name) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            return _repository.GetEntity(name);
        }

        public bool Exist(string name) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            return _repository.ExistEntity(name);
        }

        public List<G_Image> GetImages() {
            return _repository.GetEntities();
        }

        public List<G_Image> GetImages(IList<string> names) {
            if (names == null)
                throw new ArgumentNullException("names");

            return _repository.GetEntities(names);
        }

        public List<G_Image> GetContents() {
            return _repository.GetContents();
        }

        public List<G_Image> GetContents(IList<string> names) {
            if (names == null)
                throw new ArgumentNullException("names");

            return _repository.GetContents(names);
        }

        public List<G_Image> GetThumbnails() {
            return _repository.GetThumbnails();
        }

        public List<G_Image> GetThumbnails(IList<string> names) {
            if (names == null)
                throw new ArgumentNullException("names");

            return _repository.GetThumbnails(names);
        }

        public List<G_Image> GetNames() {
            return _repository.GetNames();
        }

        public List<G_Image> GetNames(IList<string> names) {
            if (names == null)
                throw new ArgumentNullException("names");

            return _repository.GetNames(names);
        }

        public void Add(params G_Image[] entities) {
            if (entities == null)
                throw new ArgumentNullException("entities");

            _repository.Insert(entities);
        }

        public void Update(params G_Image[] entities) {
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
