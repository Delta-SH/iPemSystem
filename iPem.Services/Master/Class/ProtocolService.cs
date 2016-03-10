using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Master;
using iPem.Data.Repository.Master;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

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

            List<Protocol> result = null;
            if(_cacheManager.IsSet(key)) {
                result = _cacheManager.Get<List<Protocol>>(key);
            } else {
                result = _protocolRepository.GetEntities(deviceType);
                _cacheManager.Set<List<Protocol>>(key, result);
            }

            return new PagedList<Protocol>(result, pageIndex, pageSize);
        }

        public IPagedList<Protocol> GetProtocols(int deviceType, int[] subDevTypes, int pageIndex = 0, int pageSize = int.MaxValue) {
            var key = string.Format(GlobalCacheKeys.Cs_ProtocolsInDevTypePattern, deviceType);

            List<Protocol> result = null;
            if(_cacheManager.IsSet(key)) {
                result = _cacheManager.Get<List<Protocol>>(key);
            } else {
                result = _protocolRepository.GetEntities(deviceType);
                _cacheManager.Set<List<Protocol>>(key, result);
            }

            result = result.FindAll(p => subDevTypes.Contains(p.SubDevTypeId));
            return new PagedList<Protocol>(result, pageIndex, pageSize);
        }

        public IPagedList<Protocol> GetAllProtocols(int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Protocol> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_ProtocolsRepository)) {
                result = _cacheManager.Get<List<Protocol>>(GlobalCacheKeys.Cs_ProtocolsRepository);
            } else {
                result = _protocolRepository.GetEntities();
                _cacheManager.Set<List<Protocol>>(GlobalCacheKeys.Cs_ProtocolsRepository, result);
            }

            return new PagedList<Protocol>(result, pageIndex, pageSize);
        }

        #endregion

    }
}
