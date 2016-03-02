using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using iPem.Services.Common;
using System;
using System.Collections.Generic;

namespace iPem.Services.Master {
    public partial class ProtocolService : IProtocolService {

        #region Fields

        private readonly IProtocolRepository _protocolRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ProtocolService(
            IProtocolRepository protocolRepository,
            ICacheManager cacheManager) {
            this._protocolRepository = protocolRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IPagedList<Protocol> GetProtocolsByDeviceType(int deviceType, int pageIndex = 0, int pageSize = int.MaxValue) {
            var key = string.Format(GlobalCacheKeys.Cs_ProtocolsInDevTypePattern, deviceType);

            List<Protocol> protocols = null;
            if(_cacheManager.IsSet(key)) {
                protocols = _cacheManager.Get<List<Protocol>>(key);
            } else {
                protocols = _protocolRepository.GetEntities(deviceType);
                _cacheManager.Set<List<Protocol>>(key, protocols);
            }

            var result = new PagedList<Protocol>(protocols, pageIndex, pageSize);
            return result;
        }

        public IPagedList<Protocol> GetProtocols(int deviceType, int subDevType, int pageIndex = 0, int pageSize = int.MaxValue) {
            var key = string.Format(GlobalCacheKeys.Cs_ProtocolsInDevTypeAndSubPattern, deviceType, subDevType);

            List<Protocol> protocols = null;
            if(_cacheManager.IsSet(key)) {
                protocols = _cacheManager.Get<List<Protocol>>(key);
            } else {
                protocols = _protocolRepository.GetEntities(deviceType, subDevType);
                _cacheManager.Set<List<Protocol>>(key, protocols);
            }

            var result = new PagedList<Protocol>(protocols, pageIndex, pageSize);
            return result;
        }

        public IPagedList<Protocol> GetAllProtocols(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Protocol> protocols = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_ProtocolsRepository)) {
                protocols = _cacheManager.Get<List<Protocol>>(GlobalCacheKeys.Cs_ProtocolsRepository);
            } else {
                protocols = _protocolRepository.GetEntities();
                _cacheManager.Set<List<Protocol>>(GlobalCacheKeys.Cs_ProtocolsRepository, protocols);
            }

            var result = new PagedList<Protocol>(protocols, pageIndex, pageSize);
            return result;
        }

        #endregion

    }
}
