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

        public IPagedList<Protocol> GetProtocolsByDeviceType(string deviceType, int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Protocol> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_ProtocolsRepository)) {
                var all = _cacheManager.Get<List<Protocol>>(GlobalCacheKeys.Cs_ProtocolsRepository);
                result = all.FindAll(p => p.DeviceTypeId == deviceType);
            } else {
                result = _protocolRepository.GetEntities(deviceType);
            }

            return new PagedList<Protocol>(result, pageIndex, pageSize);
        }

        public IPagedList<Protocol> GetProtocols(string deviceType, string[] subDevTypes, int pageIndex = 0, int pageSize = int.MaxValue) {
            List<Protocol> result = null;
            if(_cacheManager.IsSet(GlobalCacheKeys.Cs_ProtocolsRepository)) {
                var all = _cacheManager.Get<List<Protocol>>(GlobalCacheKeys.Cs_ProtocolsRepository);
                result = all.FindAll(p => p.DeviceTypeId == deviceType && subDevTypes.Contains(p.SubDevTypeId));
            } else {
                result = _protocolRepository.GetEntities(deviceType).FindAll(p => subDevTypes.Contains(p.SubDevTypeId));
            }

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
