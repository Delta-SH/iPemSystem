using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Rs {
    public partial class RoomService : IRoomService {

        #region Fields

        private readonly IS_RoomRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public RoomService(
            IS_RoomRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public S_Room GetRoom(string id) {
            return _repository.GetRoom(id);
        }

        public List<S_Room> GetRoomsInStation(string id) {
            return _repository.GetRoomsInStation(id);
        }

        public List<S_Room> GetRooms() {
            var key = GlobalCacheKeys.Rs_RoomsRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetItemsFromList<S_Room>(key).ToList();
            } else {
                var data = _repository.GetRooms();
                _cacheManager.AddItemsToList(key, data);
                return data;
            }
        }

        public IPagedList<S_Room> GetPagedRooms(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<S_Room>(this.GetRooms(), pageIndex, pageSize);
        }

        #endregion

    }
}
