using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
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

        public IPagedList<Room> GetAllRooms(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Room> rooms = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_RoomsRepository)) {
                rooms = _cacheManager.Get<List<Room>>(GlobalCacheKeys.Cs_RoomsRepository);
            } else {
                rooms = _roomRepository.GetEntities();
                _cacheManager.Set<List<Room>>(GlobalCacheKeys.Cs_RoomsRepository, rooms);
            }

            var result = new PagedList<Room>(rooms, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}
