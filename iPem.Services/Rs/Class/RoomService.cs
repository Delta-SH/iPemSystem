using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class RoomService : IRoomService {

        #region Fields

        private readonly IS_RoomRepository _roomRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public RoomService(
            IS_RoomRepository roomRepository,
            ICacheManager cacheManager) {
            this._roomRepository = roomRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public S_Room GetRoom(string id) {
            return _roomRepository.GetEntity(id);
        }

        public IPagedList<S_Room> GetAllRooms(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<S_Room>(this.GetAllRoomsAsList(), pageIndex, pageSize);
        }

        public List<S_Room> GetAllRoomsAsList() {
            return _roomRepository.GetEntities();
        }

        public IPagedList<S_Room> GetRooms(string parent, int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<S_Room>(this.GetRoomsAsList(parent), pageIndex, pageSize);
        }

        public List<S_Room> GetRoomsAsList(string parent) {
            return _roomRepository.GetEntities(parent);
        }

        #endregion

    }
}
