using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class RoomTypeService : IRoomTypeService {

        #region Fields

        private readonly IC_RoomTypeRepository _roomTypeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public RoomTypeService(
            IC_RoomTypeRepository roomTypeRepository,
            ICacheManager cacheManager) {
            this._roomTypeRepository = roomTypeRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_RoomType GetRoomType(string id) {
            return _roomTypeRepository.GetEntity(id);
        }

        public IPagedList<C_RoomType> GetAllRoomTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_RoomType>(this.GetAllRoomTypesAsList(), pageIndex, pageSize);
        }

        public List<C_RoomType> GetAllRoomTypesAsList() {
            List<C_RoomType> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_RoomTypesRepository)) {
                result = _cacheManager.Get<List<C_RoomType>>(GlobalCacheKeys.Rs_RoomTypesRepository);
            } else {
                result = _roomTypeRepository.GetEntities();
                _cacheManager.Set<List<C_RoomType>>(GlobalCacheKeys.Rs_RoomTypesRepository, result);
            }

            return result;
        }

        #endregion

    }
}
