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

        private readonly IFsuRepository _fsuRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FsuService(
            IFsuRepository fsuRepository,
            ICacheManager cacheManager) {
            this._fsuRepository = fsuRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public Fsu GetFsu(string id) {
            return _fsuRepository.GetEntity(id);
        }

        public IPagedList<Fsu> GetAllFsus(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Fsu>(this.GetAllFsusAsList(), pageIndex, pageSize);
        }

        public List<Fsu> GetAllFsusAsList() {
            return _fsuRepository.GetEntities();
        }

        public IPagedList<FsuExt> GetAllExtends(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<FsuExt>(this.GetAllExtendsAsList(), pageIndex, pageSize);
        }

        public List<FsuExt> GetAllExtendsAsList() {
            return _fsuRepository.GetExtends();
        }

        #endregion

    }
}
