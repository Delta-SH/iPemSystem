using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

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
            var current = _repository.GetGroup(id);
            if(current != null) {
                var type = _enumService.GetEnumById(current.TypeId);
                if(type != null) current.TypeName = type.Name;
            }
            return current;
        }

        public List<C_Group> GetGroups() {
            var result = _repository.GetGroups();
            var types = _enumService.GetEnumsByType(EnmMethodType.Group, "类型");
            for(var i = 0; i < result.Count; i++) {
                var current = result[i];
                var type = types.Find(t => t.Id == current.TypeId);
                if(type == null) continue;
                current.TypeName = type.Name;
            }
            return result;
        }

        #endregion

    }
}
