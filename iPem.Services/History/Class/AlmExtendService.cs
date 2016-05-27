using iPem.Core.Caching;
using iPem.Core.Domain.History;
using iPem.Data.Repository.History;
using System;
using System.Collections.Generic;

namespace iPem.Services.History {
    public partial class AlmExtendService : IAlmExtendService {

        #region Fields

        private readonly IAlmExtendRepository _almExtendRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public AlmExtendService(
            IAlmExtendRepository almExtendRepository,
            ICacheManager cacheManager) {
            this._almExtendRepository = almExtendRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public void Update(List<AlmExtend> entities) {
            _almExtendRepository.Update(entities);
        }

        public void UpdateConfirm(List<AlmExtend> entities) {
            _almExtendRepository.UpdateConfirm(entities);
        }

        #endregion

    }
}
