using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using System;
using System.Collections.Generic;

namespace iPem.Services.Rs {
    public partial class ProtocolService : IProtocolService {

        #region Fields

        private readonly IP_ProtocolRepository _protocolRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ProtocolService(
            IP_ProtocolRepository protocolRepository,
            ICacheManager cacheManager) {
            this._protocolRepository = protocolRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<P_Protocol> GetAllProtocols(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<P_Protocol>(this.GetAllProtocolsAsList(), pageIndex, pageSize);
        }

        public List<P_Protocol> GetAllProtocolsAsList() {
            return _protocolRepository.GetEntities();
        }

        #endregion

    }
}
