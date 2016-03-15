using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Resource;
using iPem.Data.Repository.Resource;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Resource {
    public partial class RoomTypeService : IRoomTypeService {

        #region Fields

        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public RoomTypeService(
            IRoomTypeRepository roomTypeRepository,
            ICacheManager cacheManager) {
            this._roomTypeRepository = roomTypeRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public RoomType GetRoomType(string id) {
            return _roomTypeRepository.GetEntity(id);
        }

        public IPagedList<RoomType> GetAllRoomTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<RoomType> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Rs_RoomTypesRepository)) {
                result = _cacheManager.Get<List<RoomType>>(GlobalCacheKeys.Rs_RoomTypesRepository);
            } else {
                result = _roomTypeRepository.GetEntities();
                _cacheManager.Set<List<RoomType>>(GlobalCacheKeys.Rs_RoomTypesRepository, result);
            }

            return new PagedList<RoomType>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
