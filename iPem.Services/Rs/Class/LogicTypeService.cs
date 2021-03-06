﻿using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Rs {
    public partial class LogicTypeService : ILogicTypeService {

        #region Fields

        private readonly IC_LogicTypeRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public LogicTypeService(
            IC_LogicTypeRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public C_LogicType GetLogicType(string id) {
            return _repository.GetLogicType(id);
        }

        public C_SubLogicType GetSubLogicType(string id) {
            return _repository.GetSubLogicType(id);
        }

        public List<C_LogicType> GetLogicTypes() {
            var key = GlobalCacheKeys.Rs_LogicTypesRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetItemsFromList<C_LogicType>(key).ToList();
            } else {
                var data = _repository.GetLogicTypes();
                _cacheManager.AddItemsToList(key, data);
                return data;
            }
        }

        public List<C_SubLogicType> GetSubLogicTypes() {
            var key = GlobalCacheKeys.Rs_SubLogicTypesRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetItemsFromList<C_SubLogicType>(key).ToList();
            } else {
                var data = _repository.GetSubLogicTypes();
                _cacheManager.AddItemsToList(key, data);
                return data;
            }
        }

        public IPagedList<C_LogicType> GetPagedLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_LogicType>(this.GetLogicTypes(), pageIndex, pageSize);
        }

        public IPagedList<C_SubLogicType> GetPagedSubLogicTypes(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<C_SubLogicType>(this.GetSubLogicTypes(), pageIndex, pageSize);
        }

        #endregion

    }
}
