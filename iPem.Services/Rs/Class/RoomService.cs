using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
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

        public IPagedList<Room> GetAllRooms(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Room>(this.GetAllRoomsAsList(), pageIndex, pageSize);
        }

        public List<Room> GetAllRoomsAsList() {
            return _roomRepository.GetEntities();
        }

        public IPagedList<Room> GetRooms(string parent, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<Room>(this.GetRoomsAsList(parent), pageIndex, pageSize);
        }

        public List<Room> GetRoomsAsList(string parent) {
            return _roomRepository.GetEntities(parent);
        }

        #endregion

    }
}
