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

        private readonly IA_AreaRepository _areaRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IEnumMethodsService _methodsService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AreaService(
            IA_AreaRepository areaRepository,
            ICacheManager cacheManager,
            IEnumMethodsService methodsService) {
            this._areaRepository = areaRepository;
            this._cacheManager = cacheManager;
            this._methodsService = methodsService;
        }

        #endregion

        #region Methods

        public A_Area GetArea(string id) {
            var current = _areaRepository.GetArea(id);
            if(current != null) {
                var type = _methodsService.GetValue(current.Type.Id);
                if(type != null) current.Type.Value = type.Name;
            }
            return current;
        }

        public IPagedList<A_Area> GetAreas(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<A_Area>(this.GetAreasAsList(), pageIndex, pageSize);
        }

        public List<A_Area> GetAreasAsList() {
            var result = _areaRepository.GetAreas();
            var types = _methodsService.GetValuesAsList(EnmMethodType.Area, "类型");
            for(var i = 0; i < result.Count; i++) {
                var current = result[i];
                var type = types.Find(t => t.Id == current.Type.Id);
                if(type == null) continue;
                current.Type.Value = type.Name;
            }
            return result;
        }

        #endregion

    }
}
