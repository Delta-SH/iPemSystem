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

        private readonly ID_FsuRepository _fsuRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FsuService(
            ID_FsuRepository fsuRepository,
            ICacheManager cacheManager) {
            this._fsuRepository = fsuRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public D_Fsu GetFsu(string id) {
            return _fsuRepository.GetFsu(id);
        }

        public IPagedList<D_Fsu> GetAllFsus(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<D_Fsu>(this.GetAllFsusAsList(), pageIndex, pageSize);
        }

        public List<D_Fsu> GetAllFsusAsList() {
            return _fsuRepository.GetFsus();
        }

        public D_ExtFsu GetFsuExt(string id) {
            return _fsuRepository.GetExtFsu(id);
        }

        public IPagedList<D_ExtFsu> GetAllExtends(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<D_ExtFsu>(this.GetAllExtendsAsList(), pageIndex, pageSize);
        }

        public List<D_ExtFsu> GetAllExtendsAsList() {
            return _fsuRepository.GetExtFsus();
        }

        #endregion

    }
}
