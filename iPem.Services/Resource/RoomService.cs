using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial class RoomService : IRoomService {

        #region Fields

        private readonly IRoomRepository _roomRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public RoomService(
            IRoomRepository roomRepository,
            ICacheManager cacheManager) {
            this._roomRepository = roomRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public Room GetRoom(string id) {
            return _roomRepository.GetEntity(id);
        }

        public IPagedList<Room> GetRoomsByParent(string parent, int pageIndex = 0, int pageSize = int.MaxValue) {
            var key = string.Format(GlobalCacheKeys.Rs_RoomsInParentPattern, parent);

            List<Room> rooms = null;
            if(_cacheManager.IsSet(key)) {
                rooms = _cacheManager.Get<List<Room>>(key);
            } else {
                rooms = _roomRepository.GetEntities(parent);
                _cacheManager.Set<List<Room>>(key, rooms);
            }

            var result = new PagedList<Room>(rooms, pageIndex, pageSize);
            return result;
        }

        public IPagedList<Room> GetAllRooms(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Room> rooms = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_RoomsRepository)) {
                rooms = _cacheManager.Get<List<Room>>(GlobalCacheKeys.Rs_RoomsRepository);
            } else {
                rooms = _roomRepository.GetEntities();
                _cacheManager.Set<List<Room>>(GlobalCacheKeys.Rs_RoomsRepository, rooms);
            }

            var result = new PagedList<Room>(rooms, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}
