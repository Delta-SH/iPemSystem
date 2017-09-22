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
            var key = GlobalCacheKeys.Rs_RoomsRepository;
            if (!_cacheManager.IsSet(key)) {
                return this.GetRooms().FindAll(c => c.StationId == id);
            }

            if (_cacheManager.IsHashSet(key, id)) {
                return _cacheManager.GetFromHash<List<S_Room>>(key, id);
            } else {
                var data = _repository.GetRoomsInStation(id);
                _cacheManager.SetInHash(key, id, data);
                return data;
            }
        }

        public List<S_Room> GetRooms() {
            var key = GlobalCacheKeys.Rs_RoomsRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetAllFromHash<List<S_Room>>(key).SelectMany(d => d).ToList();
            } else {
                var data = _repository.GetRooms();
                var caches = data.GroupBy(d => d.StationId).Select(d => new KeyValuePair<string, object>(d.Key, d.ToList()));
                _cacheManager.SetRangeInHash(key, caches);
                return data;
            }
        }

        public IPagedList<S_Room> GetPagedRooms(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<S_Room>(this.GetRooms(), pageIndex, pageSize);
        }

        #endregion

    }
}
