using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Core.Enum;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Rs {
    public partial class AreaService : IAreaService {

        #region Fields

        private readonly IA_AreaRepository _repository;
        private readonly ICacheManager _cacheManager;
        private readonly IEnumMethodService _enumService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AreaService(
            IA_AreaRepository repository,
            ICacheManager cacheManager,
            IEnumMethodService enumService) {
            this._repository = repository;
            this._cacheManager = cacheManager;
            this._enumService = enumService;
        }

        #endregion

        #region Methods

        public A_Area GetArea(string id) {
            var current = _repository.GetArea(id);
            if(current != null) {
                var type = _enumService.GetEnumById(current.Type.Id);
                if(type != null) current.Type.Value = type.Name;
            }
            return current;
        }

        public List<A_Area> GetAreas() {
            var result = _repository.GetAreas();
            var types = _enumService.GetEnumsByType(EnmMethodType.Area, "类型");
            for(var i = 0; i < result.Count; i++) {
                var current = result[i];
                var type = types.Find(t => t.Id == current.Type.Id);
                if(type == null) continue;
                current.Type.Value = type.Name;
            }
            return result;
        }

        public IPagedList<A_Area> GetPagedAreas(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_Area>(this.GetAreas(), pageIndex, pageSize);
        }

        #endregion

    }
}
