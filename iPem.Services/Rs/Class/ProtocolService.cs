using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class ProtocolService : IProtocolService {

        #region Fields

        private readonly IP_ProtocolRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ProtocolService(
            IP_ProtocolRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<P_Protocol> GetProtocols() {
            return _repository.GetProtocols();
        }

        public IPagedList<P_Protocol> GetPagedProtocols(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<P_Protocol>(this.GetProtocols(), pageIndex, pageSize);
        }

        #endregion

    }
}
