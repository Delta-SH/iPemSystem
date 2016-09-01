using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class RoomKeyService : IRoomKeyService {

        #region Fields

        private readonly IRoomKeyRepository _roomKeyRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public RoomKeyService(
            IRoomKeyRepository roomKeyRepository,
            ICacheManager cacheManager) {
            this._roomKeyRepository = roomKeyRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<RoomKey> GetAllKeys(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<RoomKey>(this.GetAllKeysAsList(), pageIndex, pageSize);
        }

        public List<RoomKey> GetAllKeysAsList() {
            return _roomKeyRepository.GetEntities();
        }

        #endregion

    }
}
