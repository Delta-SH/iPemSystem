﻿using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Rs {
    public partial class RoomTypeService : IRoomTypeService {

        #region Fields

        private readonly IC_RoomTypeRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public RoomTypeService(
            IC_RoomTypeRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_RoomType GetRoomType(string id) {
            return _repository.GetRoomType(id);
        }

        public List<C_RoomType> GetRoomTypes() {
            var key = GlobalCacheKeys.Rs_RoomTypesRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetItemsFromList<C_RoomType>(key).ToList();
            } else {
                var data = _repository.GetRoomTypes();
                _cacheManager.AddItemsToList(key, data);
                return data;
            }
        }

        public IPagedList<C_RoomType> GetPagedRoomTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_RoomType>(this.GetRoomTypes(), pageIndex, pageSize);
        }

        #endregion

    }
}
