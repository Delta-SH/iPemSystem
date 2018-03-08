using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Rs {
    public partial class GroupService : IGroupService {

        #region Fields

        private readonly IC_GroupRepository _repository;
        private readonly ICacheManager _cacheManager;
        private readonly IEnumMethodService _enumService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public GroupService(
            IC_GroupRepository repository,
            ICacheManager cacheManager,
            IEnumMethodService enumService) {
            this._repository = repository;
            this._cacheManager = cacheManager;
            this._enumService = enumService;
        }

        #endregion

        #region Methods

        public C_Group GetGroup(string id) {
            var data = _repository.GetGroup(id);
            var type = _enumService.GetEnumById(data.TypeId);
            if (type != null) data.TypeName = type.Name;
            return data;
        }

        public List<C_Group> GetGroups() {
            var key = GlobalCacheKeys.Rs_GroupsRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetItemsFromList<C_Group>(key).ToList();
            } else {
                var data = _repository.GetGroups();
                var types = _enumService.GetEnumsByType(EnmMethodType.Group, "类型");
                for (var i = 0; i < data.Count; i++) {
                    var current = data[i];
                    var type = types.Find(t => t.Id.Equals(current.TypeId));
                    if (type == null) continue;
                    current.TypeName = type.Name;
                }

                _cacheManager.AddItemsToList(key, data);
                return data;
            }
        }

        #endregion

    }
}
