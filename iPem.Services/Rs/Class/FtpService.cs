using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Rs {
    public class FtpService : IFtpService  {

        #region Fields

        private readonly IC_FtpRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FtpService(
            IC_FtpRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<C_Ftp> GetFtps() {
            var key = GlobalCacheKeys.Rs_FtpsRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetItemsFromList<C_Ftp>(key).ToList();
            } else {
                var data = _repository.GetEntities();
                _cacheManager.AddItemsToList(key, data);
                return data;
            }
        }

        public List<C_Ftp> GetFtps(EnmFtp type) {
            return _repository.GetEntities(type);
        }

        public IPagedList<C_Ftp> GetPagedFtps(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_Ftp>(this.GetFtps(), pageIndex, pageSize);
        }

        #endregion

    }
}
