using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Cs;
using iPem.Data.Repository.Cs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Cs {
    public partial class HIAreaService : IHIAreaService {

        #region Fields

        private readonly IH_IAreaRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public HIAreaService(
            IH_IAreaRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<H_IArea> GetAreasInTypeId(string type) {
            return _repository.GetAreasInTypeId(type);
        }

        public List<H_IArea> GetAreasInTypeName(string type) {
            return _repository.GetAreasInTypeName(type);
        }

        public List<H_IArea> GetAreasInParent(string parent) {
            return _repository.GetAreasInParent(parent);
        }

        public List<H_IArea> GetAreas() {
            return _repository.GetAreas();
        }

        public IPagedList<H_IArea> GetPagedAreas(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<H_IArea>(this.GetAreas(), pageIndex, pageSize);
        }

        #endregion
        
    }
}
