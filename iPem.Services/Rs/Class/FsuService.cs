using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

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

        public List<D_Fsu> GetFsuInRoom(string id) {
            return _repository.GetFsuInRoom(id);
        }

        public List<D_Fsu> GetFsus() {
            return _repository.GetFsus();
        }

        public D_ExtFsu GetExtFsu(string id) {
            return _repository.GetExtFsu(id);
        }

        public List<D_ExtFsu> GetExtFsus() {
            return _repository.GetExtFsus();
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
