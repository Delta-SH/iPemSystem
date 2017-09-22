using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Domain.Rs;
using iPem.Data.Repository.Rs;
using iPem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iPem.Services.Rs {
    public partial class ChannelService : IChannelService {

        #region Fields

        private readonly IV_ChannelRepository _repository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public ChannelService(
            IV_ChannelRepository repository,
            ICacheManager cacheManager) {
            this._repository = repository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public List<V_Channel> GetChannels(string camera) {
            var key = GlobalCacheKeys.Rs_ChannelsRepository;
            if (!_cacheManager.IsSet(key)) {
                return this.GetAllChannels().FindAll(c => c.CameraId == camera);
            }

            if (_cacheManager.IsHashSet(key, camera)) {
                return _cacheManager.GetFromHash<List<V_Channel>>(key, camera);
            } else {
                var data = _repository.GetEntities(camera);
                _cacheManager.SetInHash(key, camera, data);
                return data;
            }
        }

        public List<V_Channel> GetAllChannels() {
            var key = GlobalCacheKeys.Rs_ChannelsRepository;
            if (_cacheManager.IsSet(key)) {
                return _cacheManager.GetAllFromHash<List<V_Channel>>(key).SelectMany(d => d).ToList();
            } else {
                var data = _repository.GetEntities();
                var caches = data.GroupBy(d => d.CameraId).Select(d => new KeyValuePair<string, object>(d.Key, d.ToList()));
                _cacheManager.SetRangeInHash(key, caches);
                return data;
            }
        }

        public IPagedList<V_Channel> GetPagedCameras(int pageIndex = 0, int pageSize = int.MaxValue) {
            return new PagedList<V_Channel>(this.GetAllChannels(), pageIndex, pageSize);
        }

        #endregion

    }
}
