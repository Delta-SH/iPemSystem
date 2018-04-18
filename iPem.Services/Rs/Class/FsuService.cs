using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Rs {
    public partial class FsuService : IFsuService {

        #region Fields

        private readonly ID_FsuRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FsuService(
            ID_FsuRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public D_Fsu GetFsu(string id) {
            return _repository.GetFsu(id);
        }

        public List<D_Fsu> GetFsusInRoom(string id) {
            return _repository.GetFsusInRoom(id);
        }

        public List<D_Fsu> GetFsus() {
            var key = GlobalCacheKeys.Rs_FsusRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetItemsFromList<D_Fsu>(key).ToList();
            } else {
                var data = _repository.GetFsus();
                _cacheManager.AddItemsToList(key, data);
                return data;
            }
        }

        public D_ExtFsu GetExtFsu(string id) {
            return _repository.GetExtFsu(id);
        }

        public List<D_ExtFsu> GetExtFsus() {
            return _repository.GetExtFsus();
        }

        public void UpdateFsus() {
            _repository.UpdateFsus();
        }

        public void UpdateExes(params D_ExtFsu[] fsus) {
            if (fsus == null || fsus.Length == 0)
                throw new ArgumentNullException("fsus");

            _repository.UpdateExes(fsus);
        }

        public IPagedList<D_Fsu> GetPagedFsus(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<D_Fsu>(this.GetFsus(), pageIndex, pageSize);
        }

        public IPagedList<D_ExtFsu> GetPagedExtFsus(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<D_ExtFsu>(this.GetExtFsus(), pageIndex, pageSize);
        }

        #endregion

    }
}
